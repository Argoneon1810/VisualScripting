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

            Result<float> result = this.result as Result<float>;
            result.SetValue(value);
            this.result = result;
        }

        protected override int NumOfInputs()
        {
            return 0;
        }
    }
}