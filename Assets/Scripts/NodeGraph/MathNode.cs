using System;
using UnityEngine;
using UnityEngine.Events;

namespace NodeGraph
{
    public enum MathType
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Modulus,
    }

    public class MathNode : Node<float>
    {
        private static Result<float> Zero = new Result<float>();
        public UnityEvent OnTypeChange;
        [SerializeField] private MathType type;
        public MathType Type
        {
            get => type;
            set {
                type = value;
                OnTypeChange?.Invoke();
            }
        }

        protected override void Calculate()
        {
            float toReturn = 0;
            try
            {
                Result<float> a = (Children.Count - 1) >= 0 ? Children[0] ? Children[0].Tick() as Result<float> : Zero : Zero;
                Result<float> b = (Children.Count - 1) >= 1 ? Children[1] ? Children[1].Tick() as Result<float> : Zero : Zero;

                switch (type)
                {
                    case MathType.Add:
                        toReturn = a.GetValue() + b.GetValue();
                        break;
                    case MathType.Subtract:
                        toReturn = a.GetValue() - b.GetValue();
                        break;
                    case MathType.Multiply:
                        toReturn = a.GetValue() * b.GetValue();
                        break;
                    case MathType.Divide:
                        if (b.GetValue() == 0)
                            throw new DivideByZeroException();
                        toReturn = a.GetValue() / b.GetValue();
                        break;
                    case MathType.Modulus:
                        if (b.GetValue() == 0)
                            throw new DivideByZeroException();
                        toReturn = a.GetValue() % b.GetValue();
                        break;
                }
                (result as Result<float>).SetValue(toReturn);
            }
            catch(NullReferenceException e)
            {
                Debug.LogError(e.Message + "\n" + e.StackTrace);
                Debug.Log("At least one of its children is not a number node.");
            }
            catch(DivideByZeroException e)
            {
                Debug.LogError(e.Message + "\n" + e.StackTrace);
                Debug.Log("Latter child returned 0, which is invalid for " + type + "function.");
            }
        }

        protected override int NumOfInputs()
        {
            return 2;
        }
    }
}