using UnityEngine;
using EaseOfUse.VectorCalculation;
using EaseOfUse.Console;

[ExecuteInEditMode]
public class DebugPrintAnchoredPosition : MonoBehaviour
{
    void Update()
    {
        var rt = transform.rt();
        if (rt)
            Console.Print(rt.anchoredPosition);
    }
}
