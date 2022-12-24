using UnityEngine;

namespace NodeGraph
{
    public class NodeGraph : MonoBehaviour
    {
        [SerializeField] EndNode root;
        [SerializeField] int ticksPerSecond = 1;
        [SerializeField] float timeSinceLastTick = 0f;

        private void Update()
        {
            //bottom limit
            if(ticksPerSecond < 1) ticksPerSecond = 1;

            if (timeSinceLastTick > 1f / ticksPerSecond)
            {
                timeSinceLastTick = 0f;
                if (root) root.Tick();
            }
            timeSinceLastTick += Time.deltaTime;
        }
        
        public EndNode GetRoot()
        {
            return root;
        }
    }
}