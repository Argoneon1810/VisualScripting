using System;
using UnityEngine;

namespace NodeGraph.Visual
{
    [Serializable]
    public class TemporaryEdge
    {
        public Knob knobInHold;
        public Edge edgeInHold;
        public EdgeVisV2 edgeInHold_Vis;
        public Node probableParent, probableChild;

        public static implicit operator bool(TemporaryEdge self)
        {
            return self != null;
        }
    }

    [ExecuteAlways]
    public class ConnectionManagerV2 : MonoBehaviour
    {
        private static ConnectionManagerV2 instance;
        public static ConnectionManagerV2 Instance
        {
            get => instance;
            set
            {
                if (instance)
                    Destroy(value.gameObject);
                else
                {
                    instance = value;

                    if (Application.isPlaying)
                        DontDestroyOnLoad(value.gameObject);
                }
            }
        }

        [SerializeField] TemporaryEdge temporaryEdge;

        [SerializeField] float bodyWidth, outlineWidth;
        [SerializeField] Color bodyColor, outlineColor;

        [SerializeField] Transform edgeHolder;

        private void Awake()
        {
            Instance = this;
        }

        public void CreateConenction(Knob knob, KnobType knobType)
        {
            CreateTemporaryEdge(knob);

            if (knobType == KnobType.Input)
            {
                temporaryEdge.probableParent = knob.GetOwner();
                temporaryEdge.edgeInHold_Vis.EdgeStartsFrom(knob.transform);
            }
            else
            {
                temporaryEdge.probableChild = knob.GetOwner();
                temporaryEdge.edgeInHold_Vis.EdgeEndsTo(knob.transform);
            }
        }

        public void CompleteConnection(Knob knob, KnobType knobType)
        {
            //해당 클릭이 현재 노드 연결이 활성화된 상태이며
            //자신의 노드가 이미 다른 노드와 연결된 상태라면 연결을 교체하되
            //새 노드가 자기 자신 또는 자신의 부모인 경우 무시
            if (knobType == KnobType.Input && temporaryEdge.probableParent == null)
            {
                if (!HasAncestryLoop(knob.GetOwner(), temporaryEdge.probableChild))
                {
                    CompleteEdgeStretchedFromOutput(knob);
                }
            }
            else if (knobType == KnobType.Output && temporaryEdge.probableChild == null)
            {
                if (!HasAncestryLoop(temporaryEdge.probableParent, knob.GetOwner()))
                {
                    CompleteEdgeStretchedFromInput(knob);
                }
            }
        }

        private void CreateTemporaryEdge(Knob knob)
        {
            temporaryEdge = new TemporaryEdge();
            GameObject edgeGameObject = new GameObject("edge");
            Transform edgeTransform = edgeGameObject.transform;
            edgeTransform.SetParent(edgeHolder);
            temporaryEdge.knobInHold = knob;

            temporaryEdge.edgeInHold = edgeGameObject.AddComponent<Edge>();
            UIfy(knob.GetComponentInParent<Canvas>(), edgeTransform);

            edgeGameObject.AddComponent<CanvasRenderer>();
            temporaryEdge.edgeInHold_Vis = edgeGameObject.AddComponent<EdgeVisV2>();

            temporaryEdge.edgeInHold_Vis.SetVisuals(bodyWidth, bodyColor, outlineWidth, outlineColor);

            return;
        }

        private void CompleteEdgeStretchedFromOutput(Knob knob)
        {
            Node parentNode = knob.GetOwner();
            Edge oldEdge = parentNode.GetChildAt(knob.Index) as Edge;

            if (oldEdge) Destroy(oldEdge.gameObject);

            parentNode.AssignChildren(temporaryEdge.edgeInHold, knob.Index);
            temporaryEdge.edgeInHold.AssignChildren(temporaryEdge.probableChild);
            temporaryEdge.edgeInHold.name += "-" + knob.GetOwner().name + "~" + temporaryEdge.probableChild.name;
            temporaryEdge.edgeInHold_Vis.EdgeStartsFrom(knob.transform);

            ResetState();
        }

        private void CompleteEdgeStretchedFromInput(Knob knob)
        {
            Edge oldEdge = temporaryEdge.probableParent.GetChildAt(temporaryEdge.knobInHold.Index) as Edge;

            if (oldEdge) Destroy(oldEdge.gameObject);

            temporaryEdge.edgeInHold.AssignChildren(knob.GetOwner());
            temporaryEdge.probableParent.AssignChildren(temporaryEdge.edgeInHold, temporaryEdge.knobInHold.Index);
            temporaryEdge.edgeInHold.name += "-" + temporaryEdge.probableParent.name + "~" + knob.GetOwner().name;
            temporaryEdge.edgeInHold_Vis.EdgeEndsTo(knob.transform);

            ResetState();
        }

        public void TryDestroyInvalidConnection()
        {
            if (!temporaryEdge) return;

            Destroy(temporaryEdge.edgeInHold.gameObject);
            ResetState();
        }

        private void ResetState()
        {
            temporaryEdge.edgeInHold_Vis.isIncomplete = false;
            temporaryEdge = null;
        }

        private void UIfy(Canvas canvas, Transform target)
        {
            target.SetParent(canvas.transform, false);
            target.gameObject.AddComponent<RectTransform>();
        }

        private bool HasAncestryLoop(Node parent, Node child)
        {
            if (parent == child)
                return true;

            Node current = parent.GetParent();
            while (current)
            {
                if(current == child)
                    return true;
                current = current.GetParent();
            }
            return false;
        }
    }
}