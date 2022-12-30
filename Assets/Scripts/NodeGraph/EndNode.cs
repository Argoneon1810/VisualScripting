namespace NodeGraph
{
    public class EndNode : Node
    {
        protected override int NumOfInputs() => 1;

        protected override void Calculate()
        {
            if (Children.Count == 0) return;
            if (Children[0] == null) return;

            result = Children[0].Tick();
        }
    }
}