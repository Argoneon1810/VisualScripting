using UnityEngine;
using UnityEngine.UI;
using EaseOfUse.CanvasScale;

namespace NodeGraph.Visual
{
    public class UIVertexTestGraphic : Graphic
    {
        public enum VertexMode
        {
            WorldPos,
            AnchoredPosCenter,
        }

        public VertexMode mode;
        public GameObject from, to;
        public Vector3 fromPos, toPos;
        private Vector3 lastPosFrom, lastPosTo;

        private Vector2 GetPositionMatchingMode(GameObject of)
        {
            switch(mode)
            {
                default:
                case VertexMode.WorldPos:
                    return of.transform.position;
                case VertexMode.AnchoredPosCenter:
                    var rt = of.transform as RectTransform;
                    return CanvasScale.GetCenterAnchoredPosition(
                        rt.anchoredPosition,
                        rt.anchorMin,
                        rt.anchorMax,
                        rt.GetComponentInParent<Canvas>()
                    );
            }
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            fromPos = GetPositionMatchingMode(from);
            toPos = GetPositionMatchingMode(to);

            vertex.position = fromPos;
            vh.AddVert(vertex);

            vertex.position = new Vector2(fromPos.x, toPos.y);
            vh.AddVert(vertex);

            vertex.position = toPos;
            vh.AddVert(vertex);

            vertex.position = new Vector2(toPos.x, fromPos.y);
            vh.AddVert(vertex);

            vh.AddTriangle(0,1,2);
            vh.AddTriangle(0,2,3);
        }

        private void Update()
        {
            if(lastPosFrom != from.transform.position)
            {
                lastPosFrom = from.transform.position;
                SetVerticesDirty();
            }
            if(lastPosTo != to.transform.position)
            {
                lastPosTo = to.transform.position;
                SetVerticesDirty();
            }
        }
    }
}