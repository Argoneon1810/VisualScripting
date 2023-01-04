using EaseOfUse.ConsoleExpansion;
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
        private static readonly FloatResult Zero = new FloatResult();

        public UnityEvent OnTypeChange;

        Func<float, float, float> operation = (a, b) => a + b;

        [SerializeField] private MathType type = MathType.Add;
        public MathType Type
        {
            get => type;
            set
            {
                type = value;
                switch(type)
                {
                    case MathType.Add:
                        operation = (a, b) => a + b;
                        break;
                    case MathType.Subtract:
                        operation = (a, b) => a - b;
                        break;
                    case MathType.Multiply:
                        operation = (a, b) => a * b;
                        break;
                    case MathType.Divide:
                        operation = (a, b) => a / b;
                        break;
                    case MathType.Modulus:
                        operation = (a, b) => a % b;
                        break;
                }
                OnTypeChange?.Invoke();
            }
        }

        protected override void Start()
        {
            result = new FloatResult();
        }

        protected override int NumOfInputs() => 2;

        protected override void Calculate()
        {
            Result a = Children[0].Tick();
            Result b = Children[1].Tick();

            float fa, fb;

            if (!a) fa = 0;
            else if (!float.TryParse(a.GetResultInString(), out fa)) fa = float.NaN;
            if (!b) fb = 0;
            else if (!float.TryParse(b.GetResultInString(), out fb)) fb = float.NaN;

            if ((Type == MathType.Divide || Type == MathType.Modulus) && fb == 0)
                fb = float.NaN;

            if (fa == float.NaN || fb == float.NaN)
                (result as FloatResult).SetValue(float.NaN);
            else
                (result as FloatResult).SetValue(operation(fa, fb));
        }
    }
}