using System.Collections;
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

        private void Awake()
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
    }
}