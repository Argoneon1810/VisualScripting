using UnityEngine;
using UnityEngine.Events;
using EaseOfUse;

[ExecuteAlways]
public class SlideToExpand : MonoBehaviour
{
    [SerializeField] Vector2 basePosition, maxPosition;
    public float t;
    float lastT;

    Vector2 beginningPosition;
    float distance;
    bool isOpening;

    private void Start()
    {
        distance = maxPosition.x - basePosition.x;
    }

    void Update()
    {
        if(lastT != t)
        {
            lastT = t;
            transform.GetRTOrDefault().anchoredPosition = Vector2.Lerp(basePosition, maxPosition, t);
        }
    }

    public void OnBeginDrag()
    {
        isOpening = t == 0;
        beginningPosition = InputManager.GetMousePosition();
    }

    public void OnDrag()
    {
        Vector2 currentPosition = InputManager.GetMousePosition();
        if (isOpening)
            t = (currentPosition.x < beginningPosition.x) ?
                0 :
                (currentPosition.x - beginningPosition.x) / distance;
        else
            t = (currentPosition.x > beginningPosition.x) ?
                1 :
                1 - (beginningPosition.x - currentPosition.x) / distance;
    }

    public void OnEndDrag()
    {
        t = isOpening ? t > .7f ? 1 : 0 : t < .3f ? 0 : 1;
    }
}
