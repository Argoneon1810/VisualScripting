using CookNodeGraph;
using NodeGraph.Visual;
using UnityEngine;

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
        Canvas canvas;
        public TestOption testOption = TestOption.NothingType;

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
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
                (one as SingleValueNode).Value = 5;

                Node<float> two = new GameObject("two").AddComponent<SingleValueNode>();
                (two as SingleValueNode).Value = 4;

                Node<float> add = new GameObject("add").AddComponent<AddNode>();
                Node echo = new GameObject("echo").AddComponent<EchoNode>();

                UIfy(one.transform);
                UIfy(two.transform);
                UIfy(add.transform);
                UIfy(echo.transform);

                one.gameObject.AddComponent<DrawGizmosFromSelfToParentNode>();
                two.gameObject.AddComponent<DrawGizmosFromSelfToParentNode>();
                add.gameObject.AddComponent<DrawGizmosFromSelfToParentNode>();
                echo.gameObject.AddComponent<DrawGizmosFromSelfToParentNode>();

                add.AssignChildren(one);
                add.AssignChildren(two);
                echo.AssignChildren(add);
                ng.GetRoot().AssignChildren(echo);
            }
        }

        private void UIfy(Transform target)
        {
            target.SetParent(canvas.transform, false);
            target.gameObject.AddComponent<RectTransform>();
        }
    }
}