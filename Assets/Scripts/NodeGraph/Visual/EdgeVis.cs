using CookNodeGraph.Visual;
using UnityEngine;
using EaseOfUse.CanvasScale;
using System.Linq;

namespace NodeGraph.Visual
{
    public class EdgeVis : UILineRenderer
    {
        [SerializeField] Transform fromKnob, toKnob;

        [SerializeField] bool mannualInitialize;
        public bool isIncomplete = true;

        Edge self;
        Camera camera;

        Vector3 lastPos_From, lastPos_To;

        protected override void Initialize()
        {
            base.Initialize();
            camera = canvas.worldCamera ? canvas.worldCamera : Camera.main;
            self = GetComponent<Edge>();
        }

        protected void Update()
        {
            if (mannualInitialize)
            {
                mannualInitialize = false;
                MannualInitialize();
            }

            if (!camera) Initialize();

            bool isDirty = false;

            Vector3 screenPos_From;
            Vector3 screenPos_To;

            if (isIncomplete)
            {
                if (fromKnob)
                    screenPos_From = RectTransformUtility.CalculateRelativeRectTransformBounds(rootCanvas.transform, fromKnob).center;
                else
                    screenPos_From = CanvasScale.GetMousePositionInCanvas(InputManager.GetMousePosition(), rootCanvas);
                if (toKnob)
                    screenPos_To = RectTransformUtility.CalculateRelativeRectTransformBounds(rootCanvas.transform, toKnob).center;
                else
                    screenPos_To = CanvasScale.GetMousePositionInCanvas(InputManager.GetMousePosition(), rootCanvas);
            }
            else
            {
                screenPos_From = RectTransformUtility.CalculateRelativeRectTransformBounds(rootCanvas.transform, fromKnob).center;
                screenPos_To = RectTransformUtility.CalculateRelativeRectTransformBounds(rootCanvas.transform, toKnob).center;
            }
            if (lastPos_From != screenPos_From)
            {
                From = screenPos_From;
                lastPos_From = From;
                isDirty = true;
            }
            if (lastPos_To != screenPos_To)
            {
                To = screenPos_To;
                lastPos_To = To;
                isDirty = true;
            }
            if (isDirty) SetVerticesDirty();
        }

        public void EdgeStartsFrom(Transform fromKnob)
        {
            this.fromKnob = fromKnob;
        }

        public void EdgeEndsTo(Transform toKnob)
        {
            this.toKnob = toKnob;
        }

        private void MannualInitialize()
        {
            Component[] parentComp_NodeVis = self.GetParent().GetComponents<Component>().Where((comp) => comp is NodeVis).ToArray();
            Component[] childComp_NodeVis = self.GetChild().GetComponents<Component>().Where((comp) => comp is NodeVis).ToArray();
            if (parentComp_NodeVis == null || parentComp_NodeVis.Length == 0 ||
                childComp_NodeVis == null || childComp_NodeVis.Length == 0)
            {
                Debug.LogError("NodeVis must exist, but something went wrong");
                return;
            }

            fromKnob = (parentComp_NodeVis[0] as NodeVis).GetInputKnobOf(self).transform;
            toKnob = (childComp_NodeVis[0] as NodeVis).GetOutputKnobOf(self).transform;
        }
    }
}