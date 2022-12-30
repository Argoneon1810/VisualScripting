using UnityEngine;
using UnityEngine.UI;

public class SetVisibility : MonoBehaviour
{
    [SerializeField] Image target;

    public void SetVisible() => target.enabled = true;
    public void SetInvisible() => target.enabled = false;
}