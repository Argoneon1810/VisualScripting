using CookNodeGraph;

namespace NodeGraph
{
    public class EndNode : Node<Nothing>
    {
        protected override void Calculate()
        {
            if (Children.Count == 0) return;
            if (Children[0] == null) return;

            Children[0].Tick();
        }

        protected override int NumOfInputs()
        {
            return 1;
        }
    }
}