using UnityEngine;

namespace CookNodeGraph
{
    namespace Graphics
    {
        public class DynamicUILineRenderer : UILineRenderer
        {
            public RectTransform handle_From, handle_To;
            Vector2 lastPos_From, lastPos_To;

            protected override void Initialize()
            {
                base.Initialize();
                lastPos_From = lastPos_To = Vector3.zero;
            }

            protected void Update()
            {
                if (!rt) return;

                bool isDirty = false;

                var localPos_From = handle_From.anchoredPosition;

                Transform origParent = handle_To.parent;
                handle_To.SetParent(rt.parent);
                var localPos_To = handle_To.anchoredPosition;
                handle_To.SetParent(origParent);

                if (lastPos_From != localPos_From)
                {
                    From = localPos_From;
                    lastPos_From = From;
                    isDirty = true;
                }
                if (lastPos_To != localPos_To)
                {
                    To = localPos_To;
                    lastPos_To = To;
                    isDirty = true;
                }
                if (isDirty) SetVerticesDirty();
            }
        }
    }
}