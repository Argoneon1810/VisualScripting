using Toast;
using UnityEngine;

namespace NodeGraph.Visual
{
    public class EchoNodeVis : MonoBehaviour
    {
        public void OnEcho(string toEcho)
        {
            ToastManager manager = ToastManager.Instance;
            if(manager)
                manager.MakeText(toEcho, Toast.Toast.LENGTH_SHORT).Show();
        }
    }
}