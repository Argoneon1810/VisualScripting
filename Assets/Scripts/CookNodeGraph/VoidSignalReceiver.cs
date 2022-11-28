using System;

namespace CookNodeGraph
{
    public class VoidSignalReceiver : SignalReceiver<Nothing>
    {
        public event Action ToDo;
        public override void OnReceiveSignal(Nothing t)
        {
            ToDo?.Invoke(); 
        }
    }
}