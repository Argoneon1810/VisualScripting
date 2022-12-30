using UnityEngine;
using UnityEngine.UI;

public class StopScrollRect : MonoBehaviour
{
    [SerializeField] ScrollRect target;

    public void Stop()
    {
        target.StopMovement();
    }
}