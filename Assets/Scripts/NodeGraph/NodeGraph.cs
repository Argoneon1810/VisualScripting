using UnityEngine;
using UnityEngine.Events;

namespace NodeGraph
{
    public class NodeGraph : MonoBehaviour
    {
        public UnityEvent<float> Extra_OnTickDeltaChanged;

        [SerializeField] EndNode root;
        [SerializeField] int ticksPerSecond = 1;
        [SerializeField] float tickDelta = 0;

        float timeSinceLastTick = 1;

        private void Update()
        {
            timeSinceLastTick += Time.deltaTime;

            //bottom limit
            if (ticksPerSecond < 1) ticksPerSecond = 1;

            float fullTickInterval = 1f / ticksPerSecond;

            if (timeSinceLastTick > fullTickInterval)
            {
                timeSinceLastTick = 0f;
                if (root) root.Tick();
            }

            tickDelta = timeSinceLastTick / fullTickInterval;
            Extra_OnTickDeltaChanged?.Invoke(tickDelta);
        }
        
        public EndNode GetRoot()
        {
            return root;
        }
    }
}