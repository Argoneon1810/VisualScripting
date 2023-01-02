using System;
using System.Collections.Generic;

namespace IO
{
    [Serializable]
    public class Recipes
    {
        public List<Recipe> recipes;
    }

    [Serializable]
    public class Recipe
    {
        public string name;
        public List<string> ingredients;
        public List<Procedure> procesures;
        public Food Result;
    }

    [Serializable]
    public class Procedure
    {
        public int index;
        public string name, description;
        public List<Requirement> requirements;
    }

    [Serializable]
    public class Requirement
    {
        public string ingredient;
        public int quantity;
    }

    [Serializable]
    public struct Ingredient
    {
        public string name;
        public int quantity;
    }

    [Serializable]
    public struct Food
    {
        public string name;
        public float procedureIntegrity, bonus;     //(procedureIntegrity + bonus) ~= 1.0 >= perfect.
    }
}