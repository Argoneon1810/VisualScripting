using TMPro;
using UnityEngine;

using EaseOfUse.VectorCalculation;

public class DebugVis : MonoBehaviour
{
    public GameObject other;
    public TextMeshPro angleTxt, tangentTxt;
    public float angleRad;
    public float distance;
    [Range(1,200)] public float step = 1;

    Vector3 selfPos, otherPos, tan;
    float signedAngle = 0;
    public float gizmosLength = 10;

    private void OnDrawGizmos()
    {
        Vector3 tanPos = transform.position + tan * gizmosLength;
        tangentTxt.gameObject.transform.position = tanPos;
        Vector3 anglePos = selfPos + Quaternion.Euler(0, VectorCalculation.SignedAngle(selfPos, otherPos, Vector3.forward) / 2, 0) * Vector3.forward * distance * .2f;
        angleTxt.gameObject.transform.position = anglePos;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, other.transform.position);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, tanPos);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * gizmosLength);
    }

    private void Update()
    {
        angleRad += Time.deltaTime * step;
        if (angleRad < Mathf.PI) angleTxt.rectTransform.pivot = Vector2.zero;
        else angleTxt.rectTransform.pivot = Vector2.right;
        if (angleRad > 2 * Mathf.PI) angleRad = 0;

        selfPos = transform.position;
        otherPos = selfPos  + Quaternion.Euler(0, angleRad * Mathf.Rad2Deg, 0) * Vector3.forward * distance;
        signedAngle = VectorCalculation.SignedAngle(selfPos, otherPos, transform.forward);
        tan = (otherPos - selfPos).normalized.Tangent(Vector3.forward);

        other.transform.position = otherPos;
        angleTxt.text = string.Format("{0,10} {1,10:F2}\n", "angle:", signedAngle);
        tangentTxt.text = string.Format("{0,10} {1,10}\n", "tangent:", tan);
    }
}
