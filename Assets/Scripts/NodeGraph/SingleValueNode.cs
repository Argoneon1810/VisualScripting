using System.Collections;
using UnityEngine;

namespace NodeGraph
{
    public class SingleValueNode : Node<float>
    {
        public float value;

        private void Start()
        {
            Result<float> result = new Result<float>();
            result.SetValue(value);
            this.result = result;
        }

        protected override void Calculate() {}

        protected override int NumOfInputs()
        {
            return 0;
        }
    }
}