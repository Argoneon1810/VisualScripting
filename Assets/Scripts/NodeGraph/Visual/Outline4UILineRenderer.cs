using CookNodeGraph.Visual;
using UnityEngine;

namespace NodeGraph.Visual
{
    public class Outline4UILineRenderer : UILineRenderer
    {
        public void SetWidth(float width)
        {
            Width = width;
            SetVerticesDirty();
        }

        public void SetColor(Color outlineColor)
        {
            color = outlineColor;
            SetMaterialDirty();
        }

        public void SetFromTo(Vector2 fromPos, Vector2 toPos)
        {
            var normalizedDir = (fromPos - toPos).normalized;
            From = fromPos + normalizedDir * Width / 2;
            To = toPos + -normalizedDir * Width / 2;
            SetVerticesDirty();
        }
    }
}