using UnityEngine;

namespace NodeGraph
{
    public class NodeGraph : MonoBehaviour
    {
        [SerializeField] GameObject Prefab;
        [SerializeField] EndNode root;
        public bool debugNewNode;

        public GameObject CreateNewNode()
        {
            return Instantiate(Prefab, transform.parent);
        }

        private void Update()
        {
            if(debugNewNode)
            {
                debugNewNode = false;
                CreateNewNode();
            }

            if (root) root.Tick();
        }
        
        public EndNode GetRoot()
        {
            return root;
        }
    }
}