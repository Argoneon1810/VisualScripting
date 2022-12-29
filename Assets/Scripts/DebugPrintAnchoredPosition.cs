using UnityEngine;
using EaseOfUse.VectorCalculation;

[ExecuteInEditMode]
public class DebugPrintAnchoredPosition : MonoBehaviour
{
    void Update()
    {
        var rt = transform.rt();
        if (rt)
            Debug.Log(rt.anchoredPosition);
    }
}
