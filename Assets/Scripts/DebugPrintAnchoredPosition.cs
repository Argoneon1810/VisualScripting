using EaseOfUse;
using EaseOfUse.ConsoleExpansion;
using UnityEngine;

[ExecuteInEditMode]
public class DebugPrintAnchoredPosition : MonoBehaviour
{
    void Update()
    {
        var rt = transform.GetRTOrDefault();
        if (rt)
            ConsoleExpansion.Print(rt.anchoredPosition);
    }
}
