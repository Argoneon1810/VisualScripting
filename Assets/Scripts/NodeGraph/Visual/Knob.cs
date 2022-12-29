using UnityEngine;

namespace NodeGraph.Visual
{
    public enum KnobType
    {
        Input,
        Output
    }

    public class Knob : MonoBehaviour
    {
        [SerializeField] private Node self;
        [SerializeField] private KnobType type;
        [SerializeField] private int index;
        public int Index => index;

        bool pointerStaying;

        public void OnDrop()
        {
            ConnectionManagerV2.Instance.CompleteConnection(this, type);
        }

        public void OnBeginDrag()
        {
            ConnectionManagerV2.Instance.CreateConenction(this, type);
        }

        public void OnEndDrag()
        {
            ConnectionManagerV2.Instance.TryDestroyInvalidConnection();
        }

        public void OnPointerEnter()
        {
            pointerStaying = true;
        }

        public void OnPointerExit()
        {
            pointerStaying = false;
        }

        private void OnPointerStay()
        {
            //포인터가 노드 위에 머무르고 있고
            //현재 클릭이 노드 연결 활성화된 상태인 경우 노드 연결 스내핑
        }

        private void Update() { if (pointerStaying) OnPointerStay(); }

        public Node GetOwner() => self;
    }
}