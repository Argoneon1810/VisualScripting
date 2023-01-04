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
        public List<Procedure> procedures;
        public Food result;

        public class RecipeBuilder
        {
            Recipe self;
            public RecipeBuilder() => self = new Recipe();
            public RecipeBuilder WithNameOf(string name)
            {
                self.name = name;
                return this;
            }
            public RecipeBuilder WithIngredientsOf(List<string> ingredients)
            {
                self.ingredients = ingredients;
                return this;
            }
            public RecipeBuilder WithProceduresOf(List<Procedure> procedures)
            {
                self.procedures = procedures;
                return this;
            }
            public RecipeBuilder WithResultingOf(Food result)
            {
                self.result = result;
                return this;
            }
            public Recipe Build() => self;
        }
    }

    [Serializable]
    public class Procedure
    {
        public int index;
        public string name, description;
        public List<Requirement> requirements;
        //어떤 도구가 필요하고 적정 소요시간이 얼마인지 (보다 짧으면 보너스), 얼마 이상 시간을 소비하면 안되는지 (넘으면 실패, 근접할수록 마이너스) 따위가 여기 들어가야 함
        public class ProcedureBuilder
        {
            Procedure self;
            public ProcedureBuilder() => self = new Procedure();
            public ProcedureBuilder WithIndexOf(int index)
            {
                self.index = index;
                return this;
            }
            public ProcedureBuilder WithNameOf(string name)
            {
                self.name = name;
                return this;
            }
            public ProcedureBuilder WithDescriptionOf(string description)
            {
                self.description = description;
                return this;
            }
            public ProcedureBuilder WithRequirementsOf(List<Requirement> requirements)
            {
                self.requirements = requirements;
                return this;
            }
            public Procedure Build() => self;
        }
    }

    [Serializable]
    public struct Requirement
    {
        public string ingredient;
        public int quantity;
        public Requirement(string ingredient, int quantity)
        {
            this.ingredient = ingredient;
            this.quantity = quantity;
        }
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
        public Food(string name, float procedureIntegrity, float bonus)
        {
            this.name = name;
            this.procedureIntegrity = procedureIntegrity;
            this.bonus = bonus;
        }
    }
}