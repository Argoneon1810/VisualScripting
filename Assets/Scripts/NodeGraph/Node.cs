using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

namespace NodeGraph
{
    public class Node : MonoBehaviour
    {
        [SerializeField]
        protected List<Node> Children = new List<Node>();
        protected RectTransform rt;
        protected Canvas canvas;
        protected Node Parent;
        protected Result result;
        private void Awake()
        {
            rt = transform as RectTransform;
            if (!rt) gameObject.AddComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
        }

        private void OnDrawGizmos()
        {
            if (!Parent) return;
            if (!canvas) return;

            Gizmos.matrix = canvas.transform.localToWorldMatrix;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(rt.anchoredPosition, (Parent.transform as RectTransform).anchoredPosition);
        }

        public void AssignChildren(Node node)
        {
            AssignChildren(node, Children.Count);
        }

        public void AssignChildren(Node node, int index)
        {
            if (index > NumOfInputs() - 1 || index < 0)
            {
                Debug.LogError(
                    "Wrong Input Index!" + "\n" +
                    "Given: " + index + "\n" +
                    "Supposed Range: 0~" + (NumOfInputs() - 1)
                );
                return;
            }

            if (Children.Count > index)
                Children[index] = node;
            else
            {
                while (Children.Count < index)
                    Children.Add(null);
                Children.Add(node);
            }
            node.AssignParent(this);
        }

        public void AssignParent(Node node)
        {
            Parent = node;
        }

        protected virtual void Calculate()
        {
            Debug.Log(name + "_Calculate(): Function not specified");
        }

        protected virtual int NumOfInputs()
        {
            return 0;
        }

        public Result Tick()
        {
            Calculate();
            return result;
        }
    }

    public class Node<T> : Node {}
}