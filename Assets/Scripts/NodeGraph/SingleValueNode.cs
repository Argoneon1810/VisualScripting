using System.Collections;
using UnityEngine;

namespace NodeGraph
{
    public class SingleValueNode : Node<float>
    {
        [SerializeField] private float value;
        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                (result as Result<float>).SetValue(value);
            }
        }

        protected override void Start()
        {
            base.Start();
            Value = value;
        }

        protected override int NumOfInputs()
        {
            return 0;
        }
    }
}