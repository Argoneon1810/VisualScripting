using UnityEngine;

namespace Toast
{
    [ExecuteInEditMode]
    public class WrapContentWithPadding : MonoBehaviour
    {
        public Padding padding;
        private void Update()
        {
            float minX = float.MaxValue;
            float maxX = float.MinValue;
            float minY = float.MaxValue;
            float maxY = float.MinValue;
            for (int i = 0; i < transform.childCount; ++i)
            {
                RectTransform childRT = transform.GetChild(i) as RectTransform;
                Rect childRect = childRT.rect;
                if (childRect.xMin < minX) minX = childRect.xMin;
                if (childRect.xMax > maxX) maxX = childRect.xMax;
                if (childRect.yMin < minY) minY = childRect.yMin;
                if (childRect.yMax > maxY) maxY = childRect.yMax;
            }
            RectTransform rt = transform as RectTransform;
            Vector2 sizeDelta = rt.sizeDelta;
            sizeDelta.x = maxX - minX + padding.padding * 2;
            sizeDelta.y = maxY - minY + padding.padding * 2;
            rt.sizeDelta = sizeDelta;
        }

        [System.Serializable]
        public class Padding
        {
            public float padding;
        }
    }
}