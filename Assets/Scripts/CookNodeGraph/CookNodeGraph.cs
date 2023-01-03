using System;
using UnityEngine;

namespace CookNodeGraph
{
    /**
     * 접근법이 틀렸다
     * 현재 접근법을 유지하는 경우:
     *      스타트 노드에서 엔드 노드까지의 연결은 실행 라인 연결으로,
     *      매개변수 전달은 그것과 별개로 처리해야 한다
     *      이 경우 분기한 실행 연결이 다시 합쳐지는 부분이 애매하다
     *      (분기 자체는 비동기인 셈 칠 수 있으나
     *       분기해서 생긴 노드의 아웃풋을 다른 분기의 인풋이 받을 경우
     *       솔직히 어떻게 처리해야 할 지 모르겠다)
     * 접근법을 수정하는 경우:
     *      스타트 노드에서 다음 노드로 직선으로 틱을 전달하는 것이 아니라
     *      엔드 노드에서부터 역으로 인풋 연결을 타고 실행해야 한다
     *      이 경우 실행 라인이 불필요하다
     * 어느쪽이든 지금 짜인 코드로는 불가능
     */
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
            if (showDebugLogs)
                print("CookGraph:\tUpdate()\t\t\tGenerating Tick");
            OnTick?.Invoke();
        }
    }
}