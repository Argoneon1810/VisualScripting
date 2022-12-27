using System.Collections.Generic;
using UnityEngine;

namespace NodeGraph
{
    public class Node : MonoBehaviour
    {
        [SerializeField] protected Node Parent;
        [SerializeField] protected List<Node> Children = new List<Node>();

        protected Result result;

        public Node GetParent() => Parent;
        public void AssignParent(Node node)
        {
            Parent = node;
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

        public void RemoveChildren(Node node)
        {
            Children.Remove(node);
        }

        public void RemoveAllChildren()
        {
            for (int i = Children.Count - 1; i >= 0; --i)
                Children.Remove(Children[i]);
        }

        protected virtual void Calculate() { }

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

    public class Node<T> : Node
    {
        protected virtual void Start()
        {
            result = new Result<T>();
        }
    }
}