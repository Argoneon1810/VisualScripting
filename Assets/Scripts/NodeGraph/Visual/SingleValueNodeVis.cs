using EaseOfUse.ConsoleExpansion;
using TMPro;
using UnityEngine;

namespace NodeGraph.Visual
{
    public class SingleValueNodeVis : NodeVis
    {
        [SerializeField] TMP_InputField text;

        private void Start()
        {
            text.text = (self as SingleValueNode).Value.ToString();
        }

        public void OnValueChanged(string changedValue)
        {
            if (int.TryParse(changedValue, out int parsed))
            {
                (self as SingleValueNode).Value = parsed;
            }
            else if (changedValue.Equals("-") || changedValue.Equals("."))
                ConsoleExpansion.Print("Not a number, but waiting for one as it might follow afterwards.");
            else
            {
                (self as SingleValueNode).Value = 0;
                ConsoleExpansion.Print("Not a number.");
            }
        }
    }
}