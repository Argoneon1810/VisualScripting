using System.Linq;
using UnityEngine;

namespace NodeGraph.Visual
{
    [ExecuteAlways]
    public class NodeVis : MonoBehaviour
    {
        [SerializeField] protected Node self;

        protected virtual void Awake()
        {
            self = GetComponents<Component>().Where(comp => comp is Node).FirstOrDefault() as Node;
        }
    }
}