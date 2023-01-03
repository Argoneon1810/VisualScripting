using EaseOfUse.ConsoleExpansion;
using UnityEngine.Events;

namespace NodeGraph
{
    public class EchoNode : Node
    {
        public UnityEvent<string> OnEcho;
        public bool clearLogBeforePrint;

        protected override int NumOfInputs() => 1;

        protected override void Calculate()
        {
            if (Children.Count == 0) return;
            if (Children[0] == null) return;

            result = Children[0].Tick();

            if (!result) return;
            Echo(result.GetResultInString());
        }

        private void Echo(string toEcho)
        {
            if (clearLogBeforePrint)
                ConsoleExpansion.ClearLog();
            ConsoleExpansion.Print(toEcho);
            OnEcho?.Invoke(toEcho);
        }
    }
}