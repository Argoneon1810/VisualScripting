using EaseOfUse;
using EaseOfUse.BooleanTrigger;
using EaseOfUse.ConsoleExpansion;
using UnityEngine;

[ExecuteAlways]
public class AddButtonSnapper : MonoBehaviour
{
    public RectTransform ListView, ListViewContent;
    public Canvas canvas;

    [SerializeField] Vector2 lastPosListViewContent;

    [SerializeField] RectTransform rt;

    public bool refresh;

    private void Start()
    {
        rt = transform.GetRTOrDefault();
    }

    private void LateUpdate()
    {
        if (lastPosListViewContent == ListViewContent.anchoredPosition && !BooleanTrigger.Trigger(ref refresh)) return;
        lastPosListViewContent = ListViewContent.anchoredPosition;

        Bounds listViewContentBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(canvas.transform, ListViewContent);
        Vector3 probableSnapPosition
            = new Vector3(0, listViewContentBounds.min.y - listViewContentBounds.center.y - rt.sizeDelta.y / 2, 0)
            + listViewContentBounds.center;
        Bounds noBeyondBound = RectTransformUtility.CalculateRelativeRectTransformBounds(canvas.transform, ListView);
        Vector3 noBeyond = new Vector3(0, noBeyondBound.min.y - noBeyondBound.center.y, 0)
                         + noBeyondBound.center + new Vector3(0, rt.sizeDelta.y);

        rt.SetParent(canvas.transform, false);
        rt.anchoredPosition = probableSnapPosition.y < noBeyond.y ? noBeyond : probableSnapPosition;
        rt.SetParent(ListView);
    }
}
