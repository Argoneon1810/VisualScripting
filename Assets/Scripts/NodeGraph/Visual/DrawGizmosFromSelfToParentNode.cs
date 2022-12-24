using System;
using UnityEngine;

namespace NodeGraph.Visual
{
    public class DrawGizmosFromSelfToParentNode : MonoBehaviour
    {
        Node self;
        [SerializeField] bool silence;

        void Start()
        {
            self = GetComponent<Node>();
        }

        private void OnDrawGizmos()
        {
            try
            {
                if (!self.GetParent())
                {
                    if (!silence) Debug.LogError("This node has no parent to draw a line gizmo.");
                    return;
                }

                Canvas canvas = self.transform.GetComponentInParent<Canvas>();
                if (!canvas)
                {
                    if (!silence) Debug.LogError("This node has no canvas to draw a line gizmo.");
                    return;
                }

                Gizmos.matrix = canvas.transform.localToWorldMatrix;

                Gizmos.color = Color.red;
                Gizmos.DrawLine((self.transform as RectTransform).anchoredPosition, (self.GetParent().transform as RectTransform).anchoredPosition);
            }
            catch (Exception e)
            {
                if (!silence) Debug.LogError(e.Message + "\n" + e.StackTrace);
            }
        }
    }
}