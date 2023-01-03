namespace NodeGraph.Visual
{
    public class InterruptNodeVis : NodeVis
    {
        public void OnClick()
        {
            (self as InterruptNode).bInterruptResolved = true;
        }
    }
}