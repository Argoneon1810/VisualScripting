using System;
using UnityEngine;

namespace NodeGraph
{
    public class AddNode : Node<float>
    {
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
                Debug.LogError(e.Message);
            }
            finally
            {
                (result as Result<float>).SetValue(value);
            }
        }

        protected override int NumOfInputs()
        {
            return 2;
        }
    }
}