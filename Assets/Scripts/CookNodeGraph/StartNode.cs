using System;
using UnityEngine;

namespace CookNodeGraph
{
    public class StartNode : Node, IHaveOutput<Nothing>
    {
        SignalReceiver<Nothing> signalReceiver;
        public CookNodeGraph graph;

        private void Awake()
        {
            graph.OnTick += OnReceiveSignal;
        }

        public override void OnReceiveSignal()
        {
            if (showDebugLogs)
                print("StartNode:\tOnReceiveSignal()\t\tReceived Tick");
            if (showDebugLogs)
                print("StartNode:\tOnReceiveSignal()\t\tPassing over through OnStart()");
            if(signalReceiver != null)
                signalReceiver.OnReceiveSignal(Nothing.nothing);
        }

        public void AttachSignalReceiver(SignalReceiver<Nothing> receiver)
        {
            if (showDebugLogs)
                print("StartNode:\tAttachSignalReceiver()\t\tAttaching SignalReceiver");
            signalReceiver = receiver;
        }

        public void DetachSignalReceiver(SignalReceiver<Nothing> receiver)
        {
            if (showDebugLogs)
                print("StartNode:\tDetechReceiverFromEvent()\tDetaching SignalReceiver");
            if (signalReceiver.Equals(receiver))
                signalReceiver = null;
            else
                if (showDebugLogs)
                    Debug.LogError("priorly attached receiver is not equal to the SignalReceiver handed over argument");
        }

        public int Count()
        {
            return 1;
        }
    }
}