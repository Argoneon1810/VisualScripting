using UnityEngine;
using UnityEngine.Events;

namespace NodeGraph
{
    public class EchoNode : Node
    {
        public UnityEvent<string> OnEcho;

        protected override int NumOfInputs() => 1;

        protected override void Calculate()
        {
            if (Children.Count == 0) return;
            if (Children[0] == null) return;

            result = Children[0].Tick();
            Echo(result.GetResultInString());
        }

        private void Echo(string toEcho)
        {
            Debug.Log(toEcho);
            OnEcho?.Invoke(toEcho);
        }
    }
}