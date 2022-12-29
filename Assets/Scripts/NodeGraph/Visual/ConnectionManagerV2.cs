using System.Collections;
using UnityEngine;

namespace NodeGraph.Visual
{
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

        [SerializeField] Edge currentEdgeInHold;
        [SerializeField] EdgeVisV2 currentEdgeInHold_Vis;
        [SerializeField] Node probableParent, probableChild;

        [SerializeField] float bodyWidth, outlineWidth;
        [SerializeField] Color bodyColor, outlineColor;

        private void Awake()
        {
            Instance = this;
        }

        private Pair<Edge, EdgeVisV2> CreateEdge(Knob knob)
        {
            Edge edge = new GameObject("edge").AddComponent<Edge>();
            UIfy(knob.GetComponentInParent<Canvas>(), edge.transform);

            EdgeVisV2 edgeVis = edge.gameObject.AddComponent<EdgeVisV2>();
            edge.gameObject.AddComponent<CanvasRenderer>();

            edgeVis.SetVisuals(bodyWidth, bodyColor, outlineWidth, outlineColor);

            return new Pair<Edge, EdgeVisV2>(edge, edgeVis);
        }

        public void CreateConenction(Knob knob, KnobType knobType)
        {
            var edgeSet = CreateEdge(knob);
            currentEdgeInHold = edgeSet.First;
            currentEdgeInHold_Vis = edgeSet.Second;

            if (knobType == KnobType.Input)
            {
                probableParent = knob.GetOwner();
                currentEdgeInHold_Vis.EdgeStartsFrom(knob.transform);
            }
            else
            {
                probableChild = knob.GetOwner();
                currentEdgeInHold_Vis.EdgeEndsTo(knob.transform);
            }
        }

        public void CompleteConnection(Knob knob, KnobType knobType)
        {
            if (knobType == KnobType.Input && probableParent == null)
            {
                knob.GetOwner().AssignChildren(currentEdgeInHold);
                currentEdgeInHold.AssignChildren(probableChild);
                currentEdgeInHold.name += "-" + knob.GetOwner().name + "~" + probableChild.name;
                currentEdgeInHold_Vis.EdgeStartsFrom(knob.transform);
                ResetState();
            }
            else if (knobType == KnobType.Output && probableChild == null)
            {
                currentEdgeInHold.AssignChildren(knob.GetOwner());
                probableParent.AssignChildren(currentEdgeInHold);
                currentEdgeInHold.name += "-" + probableParent.name + "~" + knob.GetOwner().name;
                currentEdgeInHold_Vis.EdgeEndsTo(knob.transform);
                ResetState();
            }
        }

        public void TryDestroyInvalidConnection()
        {
            if (!currentEdgeInHold) return;

            Destroy(currentEdgeInHold.gameObject);
            ResetState();
        }

        private void ResetState()
        {
            probableParent = null;
            probableChild = null;
            currentEdgeInHold = null;
            currentEdgeInHold_Vis.isIncomplete = false;
            currentEdgeInHold_Vis = null;
        }

        private void UIfy(Canvas canvas, Transform target)
        {
            target.SetParent(canvas.transform, false);
            target.gameObject.AddComponent<RectTransform>();
        }
    }
}