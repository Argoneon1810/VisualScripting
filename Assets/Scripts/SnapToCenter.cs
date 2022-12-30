using UnityEngine;
using UnityEngine.Events;
using EaseOfUse.VectorCalculation;

public class SnapToCenter : MonoBehaviour
{
    float duration;
    float elapsed;
    Vector3 fromPosition;
    [SerializeField] AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
    public UnityEvent OnOffCentered, OnCentered, OnStartSnapping;
    bool centerNotified, offcenterNotified;

    private void Update()
    {
        if (elapsed <= 0)
        {
            if (!centerNotified)
            {
                OnCentered?.Invoke();
                centerNotified = true;
                offcenterNotified = false;
            }
            if (transform.rt().anchoredPosition != Vector2.zero)
            {
                if (!offcenterNotified)
                {
                    OnOffCentered?.Invoke();
                    offcenterNotified = true;
                    centerNotified = false;
                }
            }
            return;
        }

        elapsed -= Time.deltaTime;
        transform.rt().anchoredPosition = Vector3.Lerp(fromPosition, Vector3.zero, curve.Evaluate(1- elapsed / duration));
    }

    public void OnClick(float time)
    {
        fromPosition = transform.rt().anchoredPosition;
        duration = time;
        elapsed = time;
        OnStartSnapping?.Invoke();
    }
}
