namespace NodeGraph.Visual
{
    public class InterruptNodeVis : NodeVis
    {
        InterruptNode self;

        private void Start()
        {
            self = GetComponent<InterruptNode>();
        }

        public void OnClick()
        {
            self.bInterruptResolved = true;
        }
    }
}