using Toast;
using UnityEngine;

namespace NodeGraph.Visual
{
    public class EchoNodeVis : NodeVis
    {
        public void OnEcho(string toEcho)
        {
            ToastManager manager = ToastManager.Instance;
            if(manager)
                manager.MakeText(toEcho, Toast.Toast.LENGTH_SHORT).Show();
        }
    }
}