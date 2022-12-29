using UnityEngine;
using UnityEngine.UI;
using EaseOfUse.VectorCalculation;
using EaseOfUse.Console;
using System.Linq;
using EaseOfUse.CanvasScale;

namespace NodeGraph.Visual
{
    [ExecuteAlways]
    public class EdgeVisV2 : NodeVis
    {
        [SerializeField] Color bodyColor, outlineColor;
        [SerializeField] float width, outlineWidth;
        float lastOutlineWidth = float.NegativeInfinity;

        [SerializeField, HideInInspector] Edge self;
        [SerializeField] Canvas rootCanvas;
        [SerializeField, HideInInspector] RectTransform rectTransform;

        [SerializeField, HideInInspector] RectTransform edgeBodyTransform, edgeOutlineTransform;

        [SerializeField] Transform fromKnob, toKnob;
        [SerializeField, HideInInspector] Vector3 lastPos_From = Vector3.negativeInfinity, lastPos_To = Vector3.negativeInfinity;

        public bool isIncomplete = true;

        [SerializeField] Canvas RootCanvas
        {
            get
            {
                if (!rootCanvas)
                    rootCanvas = GetComponentInParent<Canvas>();
                return rootCanvas;
            }
        }

        [SerializeField] Transform FromKnob
        {
            get
            {
                if (!fromKnob && self.GetParent())
                    fromKnob = GetNodeVisOf(self.GetParent()).GetInputKnobAt(self.GetParent().IndexOf(self)).transform;
                return fromKnob;
            }
        }

        [SerializeField] Transform ToKnob
        {
            get
            {
                if (!toKnob && self.GetChild())
                    toKnob = GetNodeVisOf(self.GetChild()).GetOutputKnobAt(0).transform; // TODO: 출력 노드 개수가 바뀌면 수정 필요
                return toKnob;
            }
        }

        [SerializeField] RectTransform EdgeBodyTransform
        {
            get
            {
                if (!edgeBodyTransform)
                    edgeBodyTransform = transform.GetChild(1) as RectTransform;
                return edgeBodyTransform;
            }
        }

        [SerializeField]
        RectTransform EdgeOutlineTransform
        {
            get
            {
                if (!edgeOutlineTransform)
                    edgeOutlineTransform = transform.GetChild(0) as RectTransform;
                return edgeOutlineTransform;
            }
        }

        bool skipThisFrame = false;

        private void Start()
        {
            self = GetComponent<Edge>();
            rectTransform = transform as RectTransform;
        }

        private void Update()
        {
            if (transform.childCount != 2)
            {
                for (int i = transform.childCount-1; i >= 0; --i)
                    DestroyImmediate(transform.GetChild(i).gameObject);
                CreateOutline();
                CreateBody();
                skipThisFrame = true;
            }

            if (skipThisFrame)
            {
                skipThisFrame = false;
                return;
            }

            if (lastOutlineWidth != outlineWidth)
            {
                lastOutlineWidth = outlineWidth;
                EdgeOutlineTransform.offsetMin = Vector2.zero - Vector2.one * outlineWidth;
                EdgeOutlineTransform.offsetMax = Vector2.zero + Vector2.one * outlineWidth;
            }

            bool isDirty = false;

            Vector3 screenPos_From;
            Vector3 screenPos_To;

            if (isIncomplete)
            {
                if (FromKnob)
                    screenPos_From = RectTransformUtility.CalculateRelativeRectTransformBounds(RootCanvas.transform, FromKnob).center;
                else
                    screenPos_From = CanvasScale.GetMousePositionInCanvas(InputManager.GetMousePosition(), RootCanvas);
                if (ToKnob)
                    screenPos_To = RectTransformUtility.CalculateRelativeRectTransformBounds(RootCanvas.transform, ToKnob).center;
                else
                    screenPos_To = CanvasScale.GetMousePositionInCanvas(InputManager.GetMousePosition(), RootCanvas);
            }
            else
            {
                screenPos_From = RectTransformUtility.CalculateRelativeRectTransformBounds(RootCanvas.transform, FromKnob).center;
                screenPos_To = RectTransformUtility.CalculateRelativeRectTransformBounds(RootCanvas.transform, ToKnob).center;
            }

            if (lastPos_From != screenPos_From || lastPos_To != screenPos_To)
                isDirty = true;

            if (isDirty)
            {
                isDirty = false;

                Vector3 dir = screenPos_From - screenPos_To;
                Vector3 dir_Normalized = dir.normalized;
                float dir_Magnitude = dir.magnitude;
                float angleDeg = Mathf.Acos(Vector2.Dot(dir_Normalized, Vector2.right)) * Mathf.Rad2Deg;
                if (screenPos_From.y < screenPos_To.y) angleDeg *= -1;

                rectTransform.anchoredPosition = screenPos_To + dir_Normalized * dir_Magnitude / 2;
                rectTransform.localRotation = Quaternion.Euler(0, 0, angleDeg);
                rectTransform.sizeDelta = new Vector2(dir_Magnitude, width);
            }
        }

        public void EdgeStartsFrom(Transform fromKnob)
        {
            this.fromKnob = fromKnob;
        }

        public void EdgeEndsTo(Transform toKnob)
        {
            this.toKnob = toKnob;
        }

        private void CreateBody()
        {
            GameObject bodyGameObject = new GameObject();
            bodyGameObject.name = "Body";
            Transform bodyTransform = bodyGameObject.transform;
            bodyTransform.SetParent(transform, false);
            edgeBodyTransform = bodyGameObject.AddComponent<RectTransform>();
            edgeBodyTransform.anchorMin = Vector2.zero;
            edgeBodyTransform.anchorMax = Vector2.one;
            edgeBodyTransform.anchoredPosition = Vector2.zero;
            edgeBodyTransform.sizeDelta = Vector2.zero;
            Image bodyImage = bodyGameObject.AddComponent<Image>();
            bodyImage.color = bodyColor;
            bodyImage.raycastTarget = false;
        }

        private void CreateOutline()
        {
            GameObject outlineGameObject = new GameObject();
            outlineGameObject.name = "Outline";
            Transform outlineTransform = outlineGameObject.transform;
            outlineTransform.SetParent(transform, false);
            edgeOutlineTransform = outlineGameObject.AddComponent<RectTransform>();
            edgeOutlineTransform.anchorMin = Vector2.zero;
            edgeOutlineTransform.anchorMax = Vector2.one;
            edgeOutlineTransform.anchoredPosition = Vector2.zero - Vector2.one * outlineWidth;
            edgeOutlineTransform.sizeDelta = Vector2.zero - Vector2.one * outlineWidth;
            Image outlineImage = outlineGameObject.AddComponent<Image>();
            outlineImage.color = outlineColor;
            outlineImage.raycastTarget = false;
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

        public void SetVisuals(float bodyWidth, Color bodyColor, float outlineWidth, Color outlineColor)
        {
            this.width = bodyWidth;
            this.bodyColor = bodyColor;
            this.outlineWidth = outlineWidth;
            this.outlineColor = outlineColor;
        }
    }
}