using CookNodeGraph;
using UnityEngine;
using UnityEngine.Assertions;

namespace NodeGraph
{
    public enum TestOption
    {
        NothingType,
        MathType
    }

    public class NodeGraphTester : MonoBehaviour
    {
        NodeGraph ng;
        public TestOption testOption = TestOption.NothingType;

        private void Awake()
        {
            ng = GetComponent<NodeGraph>();
        }

        void Start()
        {
            if (testOption == TestOption.NothingType)
            {
                Node<Nothing> a = new GameObject("a").AddComponent<DummyNode>();
                a.transform.SetParent(transform.parent, false);

                Node<Nothing> b = new GameObject("b").AddComponent<DummyNode>();
                b.transform.SetParent(transform.parent, false);

                a.AssignChildren(b);
                ng.GetRoot().AssignChildren(a);
            }
            else if(testOption == TestOption.MathType)
            {
                Node<float> one = new GameObject("one").AddComponent<SingleValueNode>();
                (one as SingleValueNode).value = 1;

                Node<float> two = new GameObject("two").AddComponent<SingleValueNode>();
                (two as SingleValueNode).value = 2;

                Node<float> add = new GameObject("add").AddComponent<AddNode>();

                add.AssignChildren(one);
                add.AssignChildren(two);
                ng.GetRoot().AssignChildren(add);
            }
        }
    }
}