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
            //포인터가 노드 위에 머무르고 있고
            //현재 클릭이 노드 연결 활성화된 상태인 경우 노드 연결 스내핑
        }

        private void Update() { if (pointerStaying) OnPointerStay(); }

        public Node GetOwner() => self;
    }
}