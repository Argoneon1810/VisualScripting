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
            //�ش� Ŭ���� ���� ��� ������ Ȱ��ȭ�� �����̸�
            //�ڽ��� ��尡 �̹� �ٸ� ���� ����� ���¶�� ������ ��ü�ϵ�
            //�� ��尡 �ڱ� �ڽ� �Ǵ� �ڽ��� �θ��� ��� ����

            //�ϴ� �ܼ� ��� �������
            ConnectionManager.Instance.CompleteConnection(this, type);
        }

        public void OnBeginDrag()
        {
            //�ش� Ŭ���� ��� ������ Ȱ��ȭ�� ���°� �ƴ϶�� ��� ���� Ȱ��ȭ

            //�ϴ� �ܼ� ��� �������
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
            //�����Ͱ� ��� ���� �ӹ����� �ְ�
            //���� Ŭ���� ��� ���� Ȱ��ȭ�� ������ ��� ��� ���� ������
        }

        public Node GetOwner() => self;
    }
}