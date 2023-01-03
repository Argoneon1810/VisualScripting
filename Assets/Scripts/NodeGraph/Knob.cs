using NodeGraph.Visual;
using UnityEngine;

namespace NodeGraph
{
    public class Knob : Node
    {
        [SerializeField] private KnobType knobType;
        public KnobType KnobType => knobType;

        protected override void Calculate()
        {
            if (Children.Count == 0) return;
            if (Children[0] == null) return;

            result = Children[0].Tick();
        }

        protected override int NumOfInputs() => 1;
    }
}