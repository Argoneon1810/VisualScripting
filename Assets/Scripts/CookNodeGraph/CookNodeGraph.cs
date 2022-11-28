using System;
using UnityEngine;

namespace CookNodeGraph
{
    public interface IHaveInput
    {
        public abstract int Count();
    }
    public interface IHaveOutput
    {
        public abstract int Count();
    }
    public interface IHaveInput<T> : IHaveInput
    {
        public abstract SignalReceiver<T> GetSignalReceiver();
    }

    public interface IHaveOutput<T> : IHaveOutput
    {
        public abstract void AttachSignalReceiver(SignalReceiver<T> receiver);
        public abstract void DetachSignalReceiver(SignalReceiver<T> receiver);
    }

    public readonly struct Nothing
    {
        public static readonly Nothing nothing = new Nothing();
    }

    public class SignalReceiver
    {
        public static implicit operator bool(SignalReceiver exists)
        {
            return exists != null;
        }
    }
    public abstract class SignalReceiver<T> : SignalReceiver
    {
        public abstract void OnReceiveSignal(T t);
    }

    public abstract class Node : MonoBehaviour
    {
        [SerializeField] protected bool showDebugLogs = false;
        public abstract void OnReceiveSignal();
    }

    public class CookNodeGraph : MonoBehaviour
    {
        [SerializeField] protected bool showDebugLogs = false;
        public event Action OnTick;

        void Update()
        {
            if(showDebugLogs)
                print("CookGraph:\tUpdate()\t\t\tGenerating Tick");
            OnTick?.Invoke();
        }
    }
}