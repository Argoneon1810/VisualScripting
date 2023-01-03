using EaseOfUse;
using UnityEngine;
using UnityEngine.Events;

public class SnapToCenter : MonoBehaviour
{
    float duration;
    float elapsed;
    Vector3 fromPosition;
    [SerializeField] AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
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
            if (transform.GetRTOrDefault().anchoredPosition != Vector2.zero)
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
        transform.GetRTOrDefault().anchoredPosition = Vector3.Lerp(fromPosition, Vector3.zero, curve.Evaluate(1 - elapsed / duration));
    }

    public void OnClick(float time)
    {
        fromPosition = transform.GetRTOrDefault().anchoredPosition;
        duration = time;
        elapsed = time;
        OnStartSnapping?.Invoke();
    }
}
