using UnityEngine;
using UnityEngine.Animations;
using EaseOfUse.VectorCalculation;

public class ToCenter : MonoBehaviour
{
    float duration;
    float elapsed;
    Vector3 fromPosition;
    [SerializeField] AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);

    private void Update()
    {
        if (elapsed <= 0) return;

        elapsed -= Time.deltaTime;
        transform.rt().anchoredPosition = Vector3.Lerp(fromPosition, Vector3.zero, curve.Evaluate(1- elapsed / duration));
    }

    public void OnClick(float time)
    {
        fromPosition = transform.rt().anchoredPosition;
        duration = time;
        elapsed = time;
    }
}
