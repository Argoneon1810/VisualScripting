using UnityEngine;
using UnityEngine.UI;
using EaseOfUse.VectorCalculation;

namespace CookNodeGraph
{
    namespace Graphics
    {
        public class UILineRenderer : Graphic
        {
            [SerializeField] private Vector2 from;
            [SerializeField] private Vector2 to;
            [SerializeField] private float width = 10;

            public Vector2 From { get => from; protected set => from = value; }
            public Vector2 To { get => to; protected set => to = value; }
            public float Width { get => width; protected set => width = value; }

            protected RectTransform rt;

            protected virtual void Initialize()
            {
                rt = transform as RectTransform;
            }

            protected override void OnPopulateMesh(VertexHelper vh)
            {
                if (!rt) Initialize();
                vh.Clear();

                Vector2 tan = (to - from).normalized.Tangent();

                UIVertex vertex = UIVertex.simpleVert;
                vertex.color = color;

                vertex.position = from + tan * -width;
                vh.AddVert(vertex);

                vertex.position = to + tan * -width;
                vh.AddVert(vertex);

                vertex.position = to + tan * width;
                vh.AddVert(vertex);

                vertex.position = from + tan * width;
                vh.AddVert(vertex);

                vh.AddTriangle(0, 2, 1);
                vh.AddTriangle(0, 3, 2);
            }
        }
    }
}