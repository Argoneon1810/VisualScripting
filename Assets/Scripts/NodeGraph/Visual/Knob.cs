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

        bool pointerStaying;

        public void OnDrop()
        {
            //해당 클릭이 현재 노드 연결이 활성화된 상태이며
            //자신의 노드가 이미 다른 노드와 연결된 상태라면 연결을 교체하되
            //새 노드가 자기 자신 또는 자신의 부모인 경우 무시

            //일단 단순 노드 연결부터
            ConnectionManager.Instance.CompleteConnection(this, type);
        }

        public void OnBeginDrag()
        {
            //해당 클릭이 노드 연결이 활성화된 상태가 아니라면 노드 연결 활성화

            //일단 단순 노드 연결부터
            ConnectionManager.Instance.CreateConenction(this, type);
        }

        public void OnEndDrag()
        {
            ConnectionManager.Instance.TryDestroyInvalidConnection();
        }

        public void OnPointerEnter()
        {
            pointerStaying = true;
        }

        public void OnPointerExit()
        {
            pointerStaying = false;
        }

        private void Update() { if (pointerStaying) OnPointerStay(); }

        private void OnPointerStay()
        {
            //포인터가 노드 위에 머무르고 있고
            //현재 클릭이 노드 연결 활성화된 상태인 경우 노드 연결 스내핑
        }

        public Node GetOwner() => self;
    }
}