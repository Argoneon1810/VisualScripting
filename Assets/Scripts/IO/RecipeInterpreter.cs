using EaseOfUse.BooleanTrigger;
using EaseOfUse.Console;
using System.Text;
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
                Console.ClearLog();
            Console.Print("Placeholder", "Yo");
        }
    }
}