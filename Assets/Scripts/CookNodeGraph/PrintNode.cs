using CookNodeGraph;

public class PrintNode : Node, IHaveInput<Nothing>
{
    public string debugToPrint;
    SignalReceiver<Nothing> receiver;

    public int Count()
    {
        return 1;
    }

    public SignalReceiver<Nothing> GetSignalReceiver()
    {
        if (showDebugLogs)
            print("PrintNode:\tGetEventFunctionReceiver()\tHanding over VoidParamSignal");
        if (!receiver)
        {
            VoidSignalReceiver receiver = new VoidSignalReceiver();
            receiver.ToDo += OnReceiveSignal;
            this.receiver = receiver;
        }
        return receiver;
    }

    public override void OnReceiveSignal()
    {
        //print(debugToPrint);
    }
}
