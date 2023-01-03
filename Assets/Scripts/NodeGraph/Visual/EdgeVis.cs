using CookNodeGraph.Visual;
using EaseOfUse.BooleanTrigger;
using EaseOfUse.CanvasScale;
using UnityEngine;

namespace NodeGraph.Visual
{
    public class EdgeVis : UILineRenderer
    {
        [SerializeField] Transform fromKnob, toKnob;

        [SerializeField] bool mannualInitialize;
        public bool isIncomplete = true;

        Edge self;
        Camera mainCamera;

        Vector3 lastPos_From, lastPos_To;

        protected override void Initialize()
        {
            base.Initialize();
            mainCamera = canvas.worldCamera ? canvas.worldCamera : Camera.main;
            self = GetComponent<Edge>();
        }

        protected void Update()
        {
            if (BooleanTrigger.Trigger(ref mannualInitialize))
                MannualInitialize();

            if (!mainCamera) Initialize();

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
            fromKnob = self.GetParent().transform;
            toKnob = self.GetChild().transform;
        }
    }
}