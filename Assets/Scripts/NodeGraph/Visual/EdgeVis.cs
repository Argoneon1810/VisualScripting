using CookNodeGraph.Visual;
using UnityEngine;
using EaseOfUse.CanvasScale;

namespace NodeGraph.Visual
{
    public class EdgeVis : UILineRenderer
    {
        public bool isIncomplete = true;

        Edge self;
        Camera camera;

        Transform fromKnob, toKnob;

        Vector3 lastPos_From, lastPos_To;

        protected override void Initialize()
        {
            base.Initialize();
            camera = canvas.worldCamera ? canvas.worldCamera : Camera.main;
            self = GetComponent<Edge>();
        }

        public void EdgeStartsFrom(Transform fromKnob)
        {
            this.fromKnob = fromKnob;
        }

        public void EdgeEndsTo(Transform toKnob)
        {
            this.toKnob = toKnob;
        }

        protected void Update()
        {
            if (!camera) Initialize();

            bool isDirty = false;

            Vector3 screenPos_From = Vector3.zero;
            Vector3 screenPos_To = Vector3.zero;

            if (isIncomplete)
            {
                if (fromKnob)
                    screenPos_From = RectTransformUtility.CalculateRelativeRectTransformBounds(rootCanvas.transform, fromKnob).center;
                else
                    screenPos_From = CanvasScale.GetMousePositionInCanvas(InputManager.GetMousePosition(), rootCanvas);
                if(toKnob)
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
    }
}