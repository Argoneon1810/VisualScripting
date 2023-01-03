using EaseOfUse.BooleanTrigger;
using EaseOfUse.ConsoleExpansion;
using UnityEngine;

namespace IO
{
    [ExecuteAlways]
    public class RecipeInterpreter : MonoBehaviour
    {
        private const string path_Recipe = "Assets/Resources/Recipe";
        public bool convert, clearLogBeforePrint;

        void Update()
        {
            if (BooleanTrigger.Trigger(ref convert))
                Convert();
        }

        void Convert()
        {
            if (clearLogBeforePrint)
                ConsoleExpansion.ClearLog();
            ConsoleExpansion.Print("Placeholder", "Yo");
        }
    }
}