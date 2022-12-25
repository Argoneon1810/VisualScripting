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
        [SerializeField] private KnobType type;
        [SerializeField] private int index;

        bool pointerStaying;

        public void OnPointerUp()
        {
            //�ش� Ŭ���� ���� ��� ������ Ȱ��ȭ�� �����̸�
            //�ڽ��� ��尡 �̹� �ٸ� ���� ����� ���¶�� ������ ��ü�ϵ�
            //�� ��尡 �ڱ� �ڽ� �Ǵ� �ڽ��� �θ��� ��� ����
        }

        public void OnPointerDown()
        {
            //�ش� Ŭ���� ��� ������ Ȱ��ȭ�� ���°� �ƴ϶�� ��� ���� Ȱ��ȭ
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
    }
}