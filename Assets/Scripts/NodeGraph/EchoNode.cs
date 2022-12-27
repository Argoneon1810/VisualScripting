using System;
using UnityEngine;
using UnityEngine.Events;

namespace NodeGraph
{
    public enum EchoType
    {
        Text,
        Number
    }

    public class EchoNode : Node
    {
        public EchoType type;
        public UnityEvent<string> OnEcho;

        public EchoNode OfType(EchoType type)
        {
            this.type = type;
            return this;
        }

        protected override void Calculate()
        {
            if (Children.Count == 0) return;
            if (Children[0] == null) return;

            result = Children[0].Tick();
            try
            {
                switch (type)
                {
                    case EchoType.Text:
                        Echo((result as Result<string>).GetValue());
                        break;
                    case EchoType.Number:
                        Echo((result as Result<float>).GetValue().ToString());
                        break;
                }
            }
            catch (NullReferenceException e)
            {
                Echo("Please check if an EchoType is set to correct type.");
                Debug.LogError(e.Message + "\n" + e.StackTrace);
            }
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