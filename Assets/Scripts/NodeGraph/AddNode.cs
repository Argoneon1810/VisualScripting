using UnityEngine;

namespace NodeGraph
{
    public class AddNode : Node<float>
    {
        protected override void Calculate()
        {
            float value = 0;
            for (int i = 0; i < NumOfInputs(); ++i)
                value += (Children[i].Tick() as Result<float>).GetValue();
            result = new Result<float>();
            (result as Result<float>).SetValue(value);
            Debug.Log("add result: " + value);
        }

        protected override int NumOfInputs()
        {
            return 2;
        }
    }
}