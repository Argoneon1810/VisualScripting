using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CookNodeGraph
{
    namespace Graphics
    {
        public class NodeVisualizer : MonoBehaviour
        {
            Node self;
            VerticalLayoutGroup group;
            [SerializeField] float minHeight;

            private void Awake()
            {
                self = GetComponent<Node>();
                group = GetComponentInChildren<VerticalLayoutGroup>();
                var groupRT = group.transform as RectTransform;
                var element = group.transform.GetChild(0);
                var verticalMargin = Mathf.Abs(groupRT.anchoredPosition.y);
                minHeight = (element.transform as RectTransform).sizeDelta.y + verticalMargin * 2;
            }
        }
    }
}