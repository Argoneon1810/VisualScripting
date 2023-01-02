using UnityEngine;
using UnityEngine.UI;
using EaseOfUse.VectorCalculation;

namespace CookNodeGraph
{
    namespace Visual
    {
        public class UILineRenderer : Graphic
        {
            [SerializeField] private Vector2 from;
            [SerializeField] private Vector2 to;
            [SerializeField] private float width = 10;

            public Vector2 From { get => from; protected set => from = value; }
            public Vector2 To { get => to; protected set => to = value; }
            public float Width { get => width; protected set => width = value; }

            protected Canvas rootCanvas;
            public Canvas RootCanvas
            {
                get => rootCanvas;
                protected set => rootCanvas = value;
            }

            protected override void Awake()
            {
                base.Awake();
                Initialize();
            }

            protected virtual void Initialize()
            {
                RootCanvas = GetComponentInParent<Canvas>();
            }

            public void SetVisuals(Color color, float width)
            {
                this.color = color;
                Width = width;
            }

            protected override void OnPopulateMesh(VertexHelper vh)
            {
                if (!RootCanvas)
                    Initialize();

                vh.Clear();

                Vector2 tan = (to - from).normalized.Tangent();

                UIVertex vertex = UIVertex.simpleVert;
                vertex.color = color;

                vertex.position = from + tan * -width;
                vh.AddVert(vertex);

                vertex.position = to + tan * -width;
                vh.AddVert(vertex);

                vertex.position = to  + tan * width;
                vh.AddVert(vertex);

                vertex.position = from + tan * width;
                vh.AddVert(vertex);

                vh.AddTriangle(0, 2, 1);
                vh.AddTriangle(0, 3, 2);
            }
        }
    }
}