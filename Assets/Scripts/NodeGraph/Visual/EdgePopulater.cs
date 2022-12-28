using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeGraph
{
    [ExecuteInEditMode]
    public class EdgePopulater : MonoBehaviour
    {
        [SerializeField] bool Validate;

        private void Update()
        {
            if (!Validate) return;

            Object[] graphs = FindObjectsOfType(typeof(NodeGraph));
            foreach(Object graph in graphs)
            {
                NodeGraph nodeGraph = graph as NodeGraph;
                if (!nodeGraph) continue;

                ValidateConnection(nodeGraph.GetRoot());
            }
        }

        void ValidateConnection(Node parent)
        {
            List<Node> children = parent.GetChildren();

            for(int i = children.Count - 1; i >= 0; --i)
            {
                if (children[i] is Edge) continue;

                //ConnectionManager.Instance.CreateConenction();
            }
        }
    }
}