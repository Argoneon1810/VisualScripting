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
            //�����Ͱ� ��� ���� �ӹ����� �ְ�
            //���� Ŭ���� ��� ���� Ȱ��ȭ�� ������ ��� ��� ���� ������
        }

        private void Update() { if (pointerStaying) OnPointerStay(); }

        public Node GetOwner() => self;
    }
}