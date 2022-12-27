using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeGraph
{
    public class Edge : Node
    {
        protected override void Calculate()
        {
            if (Children.Count == 0)
                return;

            result = Children[0].Tick();
        }

        protected override int NumOfInputs()
        {
            return 1;
        }
    }
}
