using UnityEngine;

namespace NodeGraph.Visual
{
    public class SingleValueNodeVis : MonoBehaviour
    {
        SingleValueNode self;

        private void Start()
        {
            self = GetComponent<SingleValueNode>();
        }

        public void OnValueChanged(string changedValue)
        {
            if (int.TryParse(changedValue, out int parsed))
            {
                self.Value = parsed;
            }
            else if (changedValue.Equals("-") || changedValue.Equals("."))
                Debug.Log("Not a number, but waiting for one as it might follow afterwards.");
            else
            {
                self.Value = 0;
                Debug.Log("Not a number.");
            }
        }
    }
}