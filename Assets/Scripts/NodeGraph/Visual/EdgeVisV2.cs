using UnityEngine;
using UnityEngine.UI;
using EaseOfUse.VectorCalculation;
using EaseOfUse.Console;
using System.Linq;

namespace NodeGraph.Visual
{
    [ExecuteAlways]
    public class EdgeVisV2 : NodeVis
    {
        [SerializeField] Edge self;
        [SerializeField] Color bodyColor, outlineColor;

        [SerializeField] Canvas canvas;
        [SerializeField] RectTransform rectTransform;

        [SerializeField] RectTransform body, outline;

        [SerializeField] float width, outlineWidth;
        float lastOutlineWidth;

        private void Start()
        {
            self = GetComponent<Edge>();
            rectTransform = transform as RectTransform;
            canvas = GetComponentInParent<Canvas>();
        }

        private void CreateBody()
        {
            GameObject bodyGameObject = new GameObject();
            bodyGameObject.name = "Body";
            Transform bodyTransform = bodyGameObject.transform;
            bodyTransform.SetParent(transform, false);
            body = bodyGameObject.AddComponent<RectTransform>();
            body.anchorMin = Vector2.zero;
            body.anchorMax = Vector2.one;
            body.anchoredPosition = Vector2.zero;
            body.sizeDelta = Vector2.zero;
            Image bodyImage = bodyGameObject.AddComponent<Image>();
            bodyImage.color = bodyColor;
        }

        private void CreateOutline()
        {
            GameObject outlineGameObject = new GameObject();
            outlineGameObject.name = "Outline";
            Transform outlineTransform = outlineGameObject.transform;
            outlineTransform.SetParent(transform, false);
            outline = outlineGameObject.AddComponent<RectTransform>();
            outline.anchorMin = Vector2.zero;
            outline.anchorMax = Vector2.one;
            outline.anchoredPosition = Vector2.zero - Vector2.one * outlineWidth;
            outline.sizeDelta = Vector2.zero - Vector2.one * outlineWidth;
            Image outlineImage = outlineGameObject.AddComponent<Image>();
            outlineImage.color = outlineColor;
        }

        private void Update()
        {
            if (!outline) CreateOutline();
            if (!body) CreateBody();

            if (lastOutlineWidth != outlineWidth)
            {
                lastOutlineWidth = outlineWidth;
                outline.offsetMin = Vector2.zero - Vector2.one * outlineWidth;
                outline.offsetMax = Vector2.zero + Vector2.one * outlineWidth;
            }

            var knobInParent = GetNodeVisOf(self.GetParent()).GetInputKnobAt(self.GetParent().IndexOf(self));
            var knobInChild = GetNodeVisOf(self.GetChild()).GetOutputKnobAt(0); // TODO: 출력 노드 개수가 바뀌면 수정 필요

            var parentPos = RectTransformUtility.CalculateRelativeRectTransformBounds(canvas.transform, knobInParent.transform).center;
            var childPos = RectTransformUtility.CalculateRelativeRectTransformBounds(canvas.transform, knobInChild.transform).center;

            var dir = parentPos - childPos;
            var dir_Normalized = dir.normalized;
            var dir_Magnitude = dir.magnitude;
            var angleDeg = Mathf.Acos(Vector2.Dot(dir_Normalized, Vector2.right)) * Mathf.Rad2Deg;
            if (parentPos.y < childPos.y) angleDeg *= -1;

            rectTransform.anchoredPosition = childPos + dir_Normalized * dir_Magnitude / 2;
            rectTransform.localRotation = Quaternion.Euler(0, 0, angleDeg);
            rectTransform.sizeDelta = new Vector2(dir_Magnitude, width);
        }

        private NodeVis GetNodeVisOf(Node node)
        {
            Component[] comps_NodeVis = node.GetComponents<Component>().Where((comp) => comp is NodeVis).ToArray();
            if (comps_NodeVis == null || comps_NodeVis.Length == 0)
            {
                Debug.LogError("NodeVis must exist, but something went wrong");
                return null;
            }
            return comps_NodeVis[0] as NodeVis;
        }
    }
}