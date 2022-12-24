using System;
using UnityEngine;

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

        public EchoNode OfType(EchoType type)
        {
            this.type = type;
            return this;
        }

        protected override void Calculate()
        {
            if (Children.Count == 0) return;

            result = Children[0].Tick();
            try
            {
                switch (type)
                {
                    case EchoType.Text:
                        Debug.Log((result as Result<string>).GetValue());
                        break;
                    case EchoType.Number:
                        Debug.Log((result as Result<float>).GetValue());
                        break;
                }
            } catch (NullReferenceException e)
            {
                Debug.LogError(e.Message + "\n" + e.StackTrace);
                Debug.Log("Please check if an EchoType is set to correct type.");
            }
        }

        protected override int NumOfInputs()
        {
            return 1;
        }
    }
}