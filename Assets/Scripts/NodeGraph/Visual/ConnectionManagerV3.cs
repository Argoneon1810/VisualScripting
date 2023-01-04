using EaseOfUse;
using System;
using UnityEngine;

namespace NodeGraph.Visual
{
    [Serializable]
    public class TemporaryEdgeV2
    {
        public static Transform edgeHolder;
        public static float bodyWidth = 9, outlineWidth = 2;
        public static Color bodyColor = Color.HSVToRGB(0, 0, .16f).WithAlpha(1), outlineColor = Color.HSVToRGB(0, 0, .12f).WithAlpha(1);

        public Knob knobInHold;
        public Edge edgeInHold;
        public EdgeVisV2 edgeInHold_Vis;
        public bool isStart;

        public TemporaryEdgeV2(Knob knob, bool isStart)
        {
            knobInHold = knob;
            this.isStart = isStart;

            GameObject edgeGameObject = new GameObject("edge");
            Transform edgeTransform = edgeGameObject.transform;

            edgeTransform.SetParent(edgeHolder, false);
            edgeGameObject.AddComponent<RectTransform>();
            edgeGameObject.AddComponent<CanvasRenderer>();

            knobInHold = knob;
            edgeInHold = edgeGameObject.AddComponent<Edge>();
            edgeInHold_Vis = edgeGameObject.AddComponent<EdgeVisV2>();

            edgeInHold_Vis.SetVisuals(bodyWidth, bodyColor, outlineWidth, outlineColor);

            if (isStart) edgeInHold_Vis.EdgeStartsFrom(knob.transform);
            else edgeInHold_Vis.EdgeEndsTo(knob.transform);
        }

        public bool TryCompleteConnection(Knob knob, bool isStart)
        {
            if (isStart == this.isStart) return false;

            if (isStart)
            {
                if (!HasAncestryLoop(parent: knob, child: knobInHold))
                {
                    DestroyOldEdges(knob, knobInHold);

                    knob.AssignChildren(edgeInHold);
                    edgeInHold.AssignChildren(knobInHold);
                    edgeInHold.name += "-" + knobInHold.GetChildAt(0).name + "~" + knob.GetParent().name;
                    edgeInHold_Vis.EdgeStartsFrom(knob.transform);

                    return true;
                }
            }
            else
            {
                if (!HasAncestryLoop(parent: knobInHold, child: knob))
                {
                    DestroyOldEdges(knobInHold, knob);

                    edgeInHold.AssignChildren(knob);
                    knobInHold.AssignChildren(edgeInHold);
                    edgeInHold.name += "-" + knob.GetChildAt(0).name + "~" + knobInHold.GetParent().name;
                    edgeInHold_Vis.EdgeEndsTo(knob.transform);

                    return true;
                }
            }

            return false;
        }

        private void DestroyOldEdges(Node parent, Node child)
        {
            Edge oldEdge1, oldEdge2;
            oldEdge1 = parent.GetChildAt(0) as Edge;
            oldEdge2 = child.GetParent() as Edge;
            parent.RemoveChildAtIndexIfExists(0);
            child.RemoveParent();
            if (oldEdge1) oldEdge1.SetInvalid();
            if (oldEdge2) oldEdge2.SetInvalid();
        }

        private bool HasAncestryLoop(Node parent, Node child)
        {
            if (parent == child)
                return true;

            Node current = parent.GetParent();
            while (current)
            {
                if (current == child)
                    return true;
                current = current.GetParent();
            }
            return false;
        }

        public static implicit operator bool(TemporaryEdgeV2 self) => self != null;
    }

    public class ConnectionManagerV3 : MonoBehaviour
    {
        private static ConnectionManagerV3 instance;
        public static ConnectionManagerV3 Instance
        {
            get => instance;
            set
            {
                if (instance)
                    Destroy(value.gameObject);
                else
                {
                    instance = value;

                    TemporaryEdgeV2.edgeHolder = instance.edgeHolder;
                    TemporaryEdgeV2.bodyWidth = instance.bodyWidth;
                    TemporaryEdgeV2.bodyColor = instance.bodyColor;
                    TemporaryEdgeV2.outlineWidth = instance.outlineWidth;
                    TemporaryEdgeV2.outlineColor = instance.outlineColor;

                    if (Application.isPlaying)
                        DontDestroyOnLoad(value.gameObject);
                }
            }
        }

        [SerializeField] TemporaryEdgeV2 tempEdge;
        [SerializeField] Transform edgeHolder;
        [SerializeField] float bodyWidth, outlineWidth;
        [SerializeField] Color bodyColor, outlineColor;

        private void Awake() => Instance = this;
        public void CreateConnection(Knob knob, KnobType knobType) => tempEdge = new TemporaryEdgeV2(knob, knobType == KnobType.Input);
        public void CompleteConnection(Knob knob, KnobType knobType) => CleanupIfNoMatch(tempEdge.TryCompleteConnection(knob, knobType == KnobType.Input));
        public void TryCleanup() => CleanupIfNoMatch(false);
        public void CleanupIfNoMatch(bool isMatch)
        {
            if (isMatch) tempEdge.edgeInHold_Vis.isIncomplete = false;
            else if(tempEdge) Destroy(tempEdge.edgeInHold.gameObject);
            tempEdge = null;
        }
    }
}