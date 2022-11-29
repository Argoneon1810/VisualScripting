using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EaseOfUse.CanvasScale;

namespace CookNodeGraph
{
    namespace Graphics
    {
        public class Handle : Graphic
        {
            RectTransform rt;
            InputManager im;

            bool followMousePosition = false;

            public int vertices = 8;

            protected override void Start()
            {
                base.Start();
                Initialize();
            }

            private void Initialize()
            {
                rt = transform as RectTransform;
                im = InputManager.Instance;
                im.OnPointerDown += OnPointerDown;
                im.OnPointerUp += OnPointerUp;
            }


            protected override void OnPopulateMesh(VertexHelper vh)
            {
                if (!canvas) Initialize();

                vh.Clear();

                UIVertex vertex = UIVertex.simpleVert;
                vertex.color = color;

                var length = Mathf.Min(rt.rect.x, rt.rect.y);

                vertex.position = new Vector3(0, 0);
                vh.AddVert(vertex);
                for (int i = 0; i < vertices; ++i)
                {
                    vertex.position = Quaternion.Euler(0, 0, 360f / vertices * i) * Vector2.up * length;
                    vh.AddVert(vertex);
                }
                for (int i = 2; i <= vertices; ++i)
                {
                    vh.AddTriangle(0, i - 1, i);
                }
                vh.AddTriangle(0, vertices, 1);
            }

            public void OnPointerDown(Vector2 rawMousePosition)
            {
                if (Vector3.Distance(CanvasScale.GetMousePositionInCanvas(rawMousePosition, canvas), rt.anchoredPosition) < 50)  //50 is a 1/2 of width(or height) which is currently 100.
                    followMousePosition = true;
            }
            public void OnPointerUp(Vector2 rawMousePosition)
            {
                followMousePosition = false;

                //현 위치의 모든 rectTransform 탐색
                var gr = GetComponentInParent<GraphicRaycaster>();
                List<RaycastResult> results = new List<RaycastResult>();
                gr.Raycast(new PointerEventData(null), results);
                //node input이 발견되면 해당 노드를 SetParent
                if (results.Count > 0)
                {
                    foreach (RaycastResult result in results)
                    {
                        if (result.gameObject.TryGetComponent(out Node node))
                            transform.SetParent(result.gameObject.transform);
                        return;
                    }
                }
                //발견되지 않으면 Canvas로 SetParent
                transform.SetParent(transform.root);
            }

            private void Update()
            {
                if (followMousePosition)
                    rt.anchoredPosition = CanvasScale.GetMousePositionInCanvas(InputManager.GetMousePosition(), canvas);
            }

            protected override void OnDestroy()
            {
                base.OnDestroy();
                im.OnPointerDown -= OnPointerDown;
                im.OnPointerUp -= OnPointerUp;
            }
        }
    }
}