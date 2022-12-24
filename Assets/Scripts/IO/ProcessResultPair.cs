using System;

namespace IO
{
    public enum Process
    {
        Chopping,
    }

    [Serializable]
    public class ProcessResultPair
    {
        public Process process;
        public string result;
    }
}