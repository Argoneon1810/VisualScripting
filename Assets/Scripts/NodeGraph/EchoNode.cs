using System;
using UnityEngine;
using UnityEngine.Events;

namespace NodeGraph
{

    public class EchoNode : Node
    {
        public UnityEvent<string> OnEcho;

        protected override void Calculate()
        {
            if (Children.Count == 0) return;
            if (Children[0] == null) return;

            result = Children[0].Tick();
            var unpacked = result.GetType().GetMethod("GetValue").Invoke(result, null);
            Echo(unpacked.ToString());
        }

        private void Echo(string toEcho)
        {
            Debug.Log(toEcho);
            OnEcho?.Invoke(toEcho);
        }

        protected override int NumOfInputs()
        {
            return 1;
        }
    }
}