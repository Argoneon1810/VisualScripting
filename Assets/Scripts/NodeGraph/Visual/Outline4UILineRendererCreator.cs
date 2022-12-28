using CookNodeGraph.Visual;
using UnityEngine;

namespace NodeGraph.Visual
{
    [ExecuteAlways]
    public class Outline4UILineRendererCreator : MonoBehaviour
    {
        [SerializeField] Outline4UILineRenderer outlineRenderer;
        [SerializeField] UILineRenderer originalLineRenderer;

        [SerializeField] Material outlineMaterial;

        [SerializeField] float width;
        float lastWidth;

        [SerializeField] Color outlineColor;
        Color lastColor;

        public void AssignDefaultMaterial(Material material)
        {
            outlineMaterial = material;
        }

        private void Update()
        {
            if (!outlineRenderer)
                Initialize();

            if (lastWidth != width)
            {
                lastWidth = width;
                outlineRenderer.SetWidth(originalLineRenderer.Width + width);
            }

            if (lastColor != outlineColor)
            {
                lastColor = outlineColor;
                outlineRenderer.SetColor(outlineColor);
            }

            if (originalLineRenderer.From != outlineRenderer.From
                || originalLineRenderer.To != outlineRenderer.To)
                outlineRenderer.SetFromTo(
                    originalLineRenderer.From,
                    originalLineRenderer.To
                );
        }

        private void Initialize()
        {
            GameObject go = new GameObject();
            go.transform.SetParent(transform, false);
            go.name = "Outline";
            go.AddComponent<RectTransform>();
            go.AddComponent<CanvasRenderer>();
            outlineRenderer = go.AddComponent<Outline4UILineRenderer>();
            outlineRenderer.material = outlineMaterial;
            outlineRenderer.color = outlineColor;
            foreach (Component component in GetComponents<Component>())
                if (component is UILineRenderer)
                    originalLineRenderer = component as UILineRenderer;
        }

        public void SetVisuals(Color color, float width)
        {
            if (!outlineRenderer)
                Initialize();

            outlineColor = color;
            this.width = width;
        }

        private void OnDestroy()
        {
            DestroyImmediate(outlineRenderer.gameObject);
        }
    }
}