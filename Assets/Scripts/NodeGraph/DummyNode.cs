using CookNodeGraph;
using System.Linq;
using EaseOfUse.Console;

namespace NodeGraph
{
    public class DummyNode : Node<Nothing>
    {
        protected override void Calculate()
        {
            Console.Print(name);
            if (Children.Count() < NumOfInputs()) return;
            Children[NumOfInputs() - 1].Tick();
        }

        protected override int NumOfInputs()
        {
            return 1;
        }
    }
}