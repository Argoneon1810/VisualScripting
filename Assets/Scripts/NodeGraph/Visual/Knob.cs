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
            ConnectionManagerV2.Instance.CompleteConnection(this, type);
        }

        public void OnBeginDrag()
        {
            ConnectionManagerV2.Instance.CreateConenction(this, type);
        }

        public void OnEndDrag()
        {
            //ConnectionManager.Instance.TryDestroyInvalidConnection();     //to revert back to ConnectionManager V1
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

        private void Update() { if (pointerStaying) OnPointerStay(); }

        private void OnPointerStay()
        {
            //�����Ͱ� ��� ���� �ӹ����� �ְ�
            //���� Ŭ���� ��� ���� Ȱ��ȭ�� ������ ��� ��� ���� ������
        }

        public Node GetOwner() => self;
    }
}