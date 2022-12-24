using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace IO
{
    public class DebugIngredientParseRequester : MonoBehaviour
    {
        private const string path_Ingredient = "Assets/Resources/Ingredient";
        Dictionary<string, Ingredient> ingredients = new Dictionary<string, Ingredient>();

        [SerializeField] private bool loadGiven;
        [SerializeField] private string nameOfIngredientToLoad;

        // Use this for initialization
        void Start()
        {
            //DebugLoadSingleCarrot();
            DebugLoadAllIngredients();
        }

        private void Update()
        {
            if(loadGiven)
            {
                loadGiven = false;

                var ingredientWithName = GetIngredientByName(nameOfIngredientToLoad);
                if (!ingredientWithName.Equals(Ingredient.None))
                    Debug.Log(ingredientWithName.PrintAllToString());
                else
                    Debug.LogError("Your requested name \"" + nameOfIngredientToLoad + "\" doesn't exist in ingredient library. Check if there is any typographical error.");
            }
        }

        private Ingredient GetIngredientByName(string name)
        {
            if(ingredients.TryGetValue(name, out Ingredient ingredient))
                return ingredient;
            return Ingredient.None;
        }

        private void DebugLoadAllIngredients()
        {
            DirectoryInfo directoryInfo = Directory.CreateDirectory(path_Ingredient);

            FileInfo[] fileInfos = directoryInfo.GetFiles("*.json", SearchOption.AllDirectories);

            foreach(FileInfo fileInfo in fileInfos)
            {
                var parsed = JSONParser<Ingredient>.ReadFromJson(path_Ingredient + "/" + fileInfo.Name);
                Debug.Log(parsed.PrintAllToString());
                ingredients.Add(parsed.name, parsed);
            }
        }

        void DebugLoadSingleCarrot()
        {
            Ingredient carrot = JSONParser<Ingredient>.ReadFromJson(path_Ingredient + "/carrot.json");
            Debug.Log(carrot.PrintAllToString());
        }
    }
}