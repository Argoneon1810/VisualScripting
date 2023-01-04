using EaseOfUse.BooleanTrigger;
using EaseOfUse.ConsoleExpansion;
using System.Collections.Generic;
using UnityEngine;

namespace IO
{
    [ExecuteAlways]
    public class RecipeInterpreter : MonoBehaviour
    {
        private const string path_Recipe = "Assets/Resources/Recipe";
        public bool createFakeRecipes, readFromFakeRecipes, clearLogBeforePrint;

        void Update()
        {
            if (BooleanTrigger.Trigger(ref createFakeRecipes))
                CreateFakeRecipes();
            if (BooleanTrigger.Trigger(ref readFromFakeRecipes))
                ReadFromFakeRecipes();
        }

        void CreateFakeRecipes()
        {
            if (clearLogBeforePrint)
                ConsoleExpansion.ClearLog();
            Recipes recipes = MakeFakeRecipes();
            ConsoleExpansion.Print(JSONParser<Recipes>.SaveToJSON(path_Recipe, "FakeRecipes", recipes));
        }

        void ReadFromFakeRecipes()
        {
            Recipes recipes = JSONParser<Recipes>.ReadFromJson(path_Recipe, "FakeRecipes");
            ConsoleExpansion.Print(recipes);
        }

        Recipes MakeFakeRecipes()
        {
            Recipes recipes = new Recipes();
            recipes.recipes = new List<Recipe>
            {
                new Recipe.RecipeBuilder()
                    .WithNameOf("Barrel Roll")
                    .WithIngredientsOf(new List<string> { "carrot", "raddish" })
                    .WithProceduresOf(new List<Procedure>
                    {
                        new Procedure.ProcedureBuilder()
                            .WithIndexOf(0)
                            .WithNameOf("Put in a dish")
                            .WithDescriptionOf("Yanamsein")
                            .WithRequirementsOf(new List<Requirement> {
                                new Requirement("carrot", 1),
                                new Requirement("raddish", 1)
                            })
                            .Build()
                    })
                    .WithResultingOf(new Food("Barrel Roll", 0.99f, 0.01f))
                    .Build()
            };
            return recipes;
        }
    }
}