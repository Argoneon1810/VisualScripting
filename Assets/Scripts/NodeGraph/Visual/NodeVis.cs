using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NodeGraph.Visual
{
    [ExecuteAlways]
    public class NodeVis : MonoBehaviour
    {
        [SerializeField] List<Knob> inputKnobs = new List<Knob>();
        [SerializeField] List<Knob> outputKnobs = new List<Knob>();

        protected virtual void Awake()
        {
            if (inputKnobs.Count == 0 || outputKnobs.Count == 0)
            {
                Knob[] knobs = GetComponentsInChildren<Knob>();

                if (inputKnobs.Count == 0)
                {
                    inputKnobs = new List<Knob>(knobs
                        .Where((k) => k.gameObject.name.Contains("In"))
                        .OrderBy((k) => k.gameObject.name)
                        .ToArray()
                    );
                }
                if (outputKnobs.Count == 0)
                {
                    outputKnobs = new List<Knob>(knobs
                        .Where((k) => k.gameObject.name.Contains("Out"))
                        .OrderBy((k) => k.gameObject.name)
                        .ToArray()
                    );
                }
            }
        }

        public Knob GetInputKnobAt(int index)
        {
            if (index <= inputKnobs.Count-1)
                return inputKnobs[index];
            return null;
        }

        public Knob GetOutputKnobAt(int index)
        {
            if (index <= outputKnobs.Count - 1)
                return outputKnobs[index];
            return null;
        }

        public Knob GetInputKnobOf(Node node)
        {
            Knob[] knobs = inputKnobs.Where((knob) => knob.GetOwner().HasChild(node)).ToArray();
            if (knobs.Length > 0)
                return knobs[0];
            return null;
        }

        public Knob GetOutputKnobOf(Node node)
        {
            Knob[] knobs = outputKnobs.Where((knob) => knob.GetOwner().GetParent() == node).ToArray(); // TODO: 출력 노드 개수가 바뀌면 수정 필요
            if (knobs.Length > 0)
                return knobs[0];
            return null;
        }
    }
}