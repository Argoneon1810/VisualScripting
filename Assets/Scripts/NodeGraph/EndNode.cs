using CookNodeGraph;

namespace NodeGraph
{
    public class EndNode : Node<Nothing>
    {
        protected override void Calculate()
        {
            Children[NumOfInputs() - 1]?.Tick();
        }

        protected override int NumOfInputs()
        {
            return 1;
        }
    }
}