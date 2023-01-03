using CookNodeGraph;
using EaseOfUse.ConsoleExpansion;
using System.Linq;

namespace NodeGraph
{
    public class DummyNode : Node<Nothing>
    {
        protected override void Calculate()
        {
            ConsoleExpansion.Print(name);
            if (Children.Count() < NumOfInputs()) return;
            Children[NumOfInputs() - 1].Tick();
        }

        protected override int NumOfInputs()
        {
            return 1;
        }
    }
}