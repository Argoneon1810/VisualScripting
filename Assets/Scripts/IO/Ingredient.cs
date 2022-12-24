using System;
using System.Collections.Generic;
using System.Text;

namespace IO
{
    [Serializable]
    public struct Ingredient
    {
        public static Ingredient None = new Ingredient("none", -1, null);

        public string name;
        public int quantity;
        public List<ProcessResultPair> resultsOfProcessing;

        public Ingredient(string name, int quantity, List<ProcessResultPair> resultsOfProcessing)
        {
            this.name = name;
            this.quantity = quantity;
            this.resultsOfProcessing = resultsOfProcessing;
        }

        public string PrintAllToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("name:" + name);
            sb.Append(",\n");
            sb.Append("quantity:" + quantity);
            sb.Append(",\n");
            sb.Append("resultOfProcessing:{" + GetAllResultsOfProcessingInString() + "}");
            return sb.ToString();
        }

        private string GetAllResultsOfProcessingInString()
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (var resultOfProcessing in resultsOfProcessing)
            {
                if (count != 0) sb.Append(", ");
                sb.Append(resultOfProcessing.process + ":" + resultOfProcessing.result);
                Console.WriteLine(resultOfProcessing.process + ":" + resultOfProcessing.result);
                count++;
            }
            return sb.ToString();
        }
    }
}