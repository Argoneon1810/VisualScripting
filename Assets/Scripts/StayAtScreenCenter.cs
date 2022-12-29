using UnityEngine;
using EaseOfUse.VectorCalculation;

[ExecuteAlways]
public class StayAtScreenCenter : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    void Update()
    {
        (transform as RectTransform).anchoredPosition -= RectTransformUtility.CalculateRelativeRectTransformBounds(canvas.transform, transform).center.ToVector2XY();
    }
}
