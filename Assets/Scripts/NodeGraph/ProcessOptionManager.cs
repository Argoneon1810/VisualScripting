using EaseOfUse.ConsoleExpansion;
using System.Collections.Generic;
using UnityEngine;

namespace NodeGraph
{
    public interface IProcessOption
    {
        string Name { get; internal set; }
    }

    public class ProcessOption : IProcessOption
    {
        private string name;
        string IProcessOption.Name
        {
            get => name;
            set => name = value;
        }
    }

    public class ProcessOptionManager : MonoBehaviour
    {
        public static ProcessOptionManager Instance;

        Dictionary<string, ProcessOption> processOptions;

        private void Awake()
        {
            if (Instance)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public ProcessOption GetProcessOption(string name)
        {
            if (processOptions.TryGetValue(name, out ProcessOption processOption))
                return processOption;
            return null;
        }

        public void EnlistNewProcessOption(string name, ProcessOption processOptionToAdd)
        {
            if (processOptions.ContainsKey(name))
            {
                ConsoleExpansion.PrintError("Key already exists");
                return;
            }
            if (processOptionToAdd == null)
            {
                ConsoleExpansion.PrintError("Value is null");
                return;
            }
            processOptions.Add(name, processOptionToAdd);
        }
    }
}