using System;
using UnityEngine;

namespace CookNodeGraph
{
    public class Edge<T> : MonoBehaviour
    {
        [SerializeField] protected bool showDebugLogs = false;

        [SerializeField] Node left, right;
        [SerializeField] SignalReceiver<T> lastReceiver;

        public bool debugAttach = false;
        public Node debugLeft, debugRight;

        private void Update()
        {
            if(debugAttach)
            {
                debugAttach = false;
                TryAttachLeft(debugLeft);
                TryAttachRight(debugRight);
            }
        }

        public bool TryAttachLeft(Node node)
        {
            if(node is IHaveOutput<T>)
            {
                left = node;
                OnAttach();
                return true;
            }
            return false;
        }

        public bool TryAttachRight(Node node)
        {
            if (node is IHaveInput<T>)
            {
                right = node;
                OnAttach();
                return true;
            }
            return false;
        }

        public virtual void OnAttach()
        {
            if (!left || !right) return;
            if (showDebugLogs)
                print("Edge:\t\tOnAttach()\t\t\tAttached both successfully");
            lastReceiver = (right as IHaveInput<T>).GetSignalReceiver();
            if (showDebugLogs)
                print("Edge:\t\tOnAttach()\t\t\tVoidParamSignal Received");
            (left as IHaveOutput<T>).AttachSignalReceiver(lastReceiver);
            if (showDebugLogs)
                print("Edge:\t\tOnAttach()\t\t\tAttached delegate from right to left");
        }

        public virtual void OnDetach()
        {
            (left as IHaveOutput<T>).DetachSignalReceiver(lastReceiver);
            if (showDebugLogs)
                print("Edge:\t\tOnAttach()\t\t\tDetached delegate from right to left");
        }
    }
}