using UnityEngine;

namespace CookNodeGraph
{
    namespace Visual
    {
        public class DynamicUILineRenderer : UILineRenderer
        {
            public RectTransform handle_From, handle_To;
            Vector3 lastPos_From, lastPos_To;
            Camera mainCamera;

            protected override void Initialize()
            {
                base.Initialize();
                mainCamera = canvas.worldCamera ? canvas.worldCamera : Camera.main;
            }

            protected void Update()
            {
                if (!mainCamera) Initialize();

                bool isDirty = false;

                var screenPos_From = RectTransformUtility.CalculateRelativeRectTransformBounds(RootCanvas.transform, handle_From).center;
                var screenPos_To = RectTransformUtility.CalculateRelativeRectTransformBounds(RootCanvas.transform, handle_To).center;

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
}