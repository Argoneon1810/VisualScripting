using EaseOfUse.CanvasScale;
using UnityEngine;

namespace CookNodeGraph
{
    namespace Visual
    {
        public class EdgeStartPoint : MonoBehaviour
        {
            InputManager im;
            RectTransform rt;
            Canvas rootCanvas;
            
            void Start()
            {
                im = InputManager.Instance;
                rt = transform as RectTransform;
                rootCanvas = rt.GetComponentInParent<Canvas>();
                im.OnPointerDown += OnPointerDown;
            }

            private void OnPointerDown(Vector2 rawMousePosition)
            {
                var canvasSelfPos = RectTransformUtility.WorldToScreenPoint(rootCanvas.worldCamera, rt.position);
                var distance = Vector2.Distance(rawMousePosition, canvasSelfPos);
                if (distance < rt.sizeDelta.x / 2)
                    print("test");
            }
        }
    }
}