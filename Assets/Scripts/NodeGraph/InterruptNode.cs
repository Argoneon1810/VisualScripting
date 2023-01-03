using EaseOfUse.BooleanTrigger;

namespace NodeGraph
{
    public class InterruptNode : Node
    {
        //private static readonly Result Empty = new Result();

        protected override Result Result
        {
            get
            {
                Result toReturn = result;
                //result = Empty;
                result = null;
                return toReturn;
            }
        }

        public bool bInterruptResolved = false;

        protected override int NumOfInputs() => 1;

        protected override void Calculate()
        {
            if (Children.Count == 0) return;
            if (Children[0] == null) return;

            if (BooleanTrigger.Trigger(ref bInterruptResolved))
            {
                result = Children[0].Tick();
            }
            //else result = Empty;
        }
    }
}