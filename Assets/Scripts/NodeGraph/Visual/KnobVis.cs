namespace NodeGraph.Visual
{
    public enum KnobType
    {
        Input,
        Output
    }

    public class KnobVis : NodeVis
    {
        bool pointerStaying;

        public void OnDrop()
        {
            ConnectionManagerV3.Instance.CompleteConnection((self as Knob), (self as Knob).KnobType);
        }

        public void OnBeginDrag()
        {
            ConnectionManagerV3.Instance.CreateConnection((self as Knob), (self as Knob).KnobType);
        }

        public void OnEndDrag()
        {
            ConnectionManagerV3.Instance.TryCleanup();
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