using UnityEngine;
using TMPro;
using EaseOfUse.Console;

namespace NodeGraph.Visual
{
    public class SingleValueNodeVis : NodeVis
    {
        SingleValueNode self;
        [SerializeField] TMP_InputField text;

        private void Start()
        {
            self = GetComponent<SingleValueNode>();
            text.text = self.Value.ToString();
        }

        public void OnValueChanged(string changedValue)
        {
            if (int.TryParse(changedValue, out int parsed))
            {
                self.Value = parsed;
            }
            else if (changedValue.Equals("-") || changedValue.Equals("."))
                Console.Print("Not a number, but waiting for one as it might follow afterwards.");
            else
            {
                self.Value = 0;
                Console.Print("Not a number.");
            }
        }
    }
}