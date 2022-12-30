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
                (result as FloatResult).SetValue(value);
            }
        }

        protected override int NumOfInputs() => 0;

        protected override void Start()
        {
            result = new FloatResult(value);
        }
    }
}