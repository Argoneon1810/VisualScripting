using System;

namespace NodeGraph
{
    public class AddNode : Node<float>
    {
        protected override void Start()
        {
            result = new FloatResult();
        }

        protected override int NumOfInputs() => 2;

        protected override void Calculate()
        {
            float value = 0;
            try
            {
                for (int i = 0; i < NumOfInputs(); ++i)
                    value += (Children[i].Tick() as Result<float>).GetValue();
            }
            catch (NullReferenceException e)
            {
                EaseOfUse.Console.Console.PrintError(e.Message);
            }
            finally
            {
                (result as Result<float>).SetValue(value);
            }
        }
    }
}