using UnityEngine;
using EaseOfUse.BooleanTrigger;

namespace NodeGraph
{
    public class Edge : Node
    {
        protected override void Calculate()
        {
            if (Children.Count == 0) return;
            result = Children[0].Tick();
        }

        protected override int NumOfInputs()
        {
            return 1;
        }

        public Node GetChild()
        {
            return Children.Count > 0 ? Children[0] : null;
        }

        public void SetInvalid()
        {
            Destroy(gameObject);
        }

#if UNITY_EDITOR_WIN
        [SerializeField] bool bInvalidate;

        private void Update()
        {
            if (BooleanTrigger.Trigger(ref bInvalidate))
            {
                GetParent().RemoveChildAtIndexIfExists(0);
                GetChild().RemoveParent();
                SetInvalid();
            }
        }
#endif
    }
}
