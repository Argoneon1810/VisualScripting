using System.Collections;
using UnityEngine;

namespace NodeGraph
{
    public class SingleValueNode : Node<float>
    {
        public float value;

        protected override void Start()
        {
            base.Start();
            SetValue(value);
        }

        private void SetValue(float value)
        {
            Result<float> result = this.result as Result<float>;
            result.SetValue(value);
            this.result = result;
        }

        public void OnValueChanged(string changedValue)
        {
            if (int.TryParse(changedValue, out int parsed))
            {
                value = parsed;
                SetValue(value);
            }
            else if (changedValue.Equals("-") || changedValue.Equals("."))
                Debug.Log("Not a number, but waiting for one as it might follow afterwards.");
            else
            {
                value = 0;
                SetValue(value);
                Debug.Log("Not a number.");
            }
        }

        protected override int NumOfInputs()
        {
            return 0;
        }
    }
}