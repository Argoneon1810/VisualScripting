using CookNodeGraph;
using System.Linq;
using UnityEngine;

namespace NodeGraph
{
    public class DummyNode : Node<Nothing>
    {
        protected override void Calculate()
        {
            Debug.Log(name);
            if (Children.Count() < NumOfInputs()) return;
            Children[NumOfInputs() - 1].Tick();
        }

        protected override int NumOfInputs()
        {
            return 1;
        }
    }
}