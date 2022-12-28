using UnityEngine;
using UnityEngine.UI;

namespace NodeGraph.Visual
{
    public class EndNodeVis : NodeVis
    {
        public Color accentColor, idleColor;
        [SerializeField] private Image indicator_image;

        public void OnTickDeltaChanged(float delta)
        {
            indicator_image.color = Color.Lerp(accentColor, idleColor, delta);
        }
    }
}