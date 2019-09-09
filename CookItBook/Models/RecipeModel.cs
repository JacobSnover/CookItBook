using CookItBook.Infrastructure;
using System.Collections.Generic;

namespace CookItBook.Models
{
    public class RecipeModel
    {
        public RecipeModel()
        {
            Ingredients = new List<IngredientModel>();
            Instructions = new List<InstructionModel>();
        }

        public List<IngredientModel> Ingredients { get; set; }
        public List<InstructionModel> Instructions { get; set; }
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public bool GlutenFree { get; set; }
        public bool DairyFree { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool Nuts { get; set; }

        public static RecipeModel DtoToModel(Recipe recipe)
        {
            return new RecipeModel()
            {
                RecipeID = recipe.RecipeID,
                Name = recipe.Name.Trim(),
                GlutenFree = recipe.GlutenFree ?? false,
                DairyFree = recipe.DairyFree ?? false,
                Vegetarian = recipe.Vegetarian ?? false,
                Vegan = recipe.Vegan ?? false,
                Nuts = recipe.Nuts ?? false,
                Ingredients = IngredientModel.DtoToModelToList(recipe.Ingredient),
                Instructions = InstructionModel.DtoToModelToList(recipe.Instruction)
            };
        }

        public static Recipe ModelToDto(RecipeModel recipeModel)
        {
            return new Recipe()
            {
                RecipeID = recipeModel.RecipeID,
                Name = recipeModel.Name.Trim(),
                GlutenFree = recipeModel.GlutenFree,
                DairyFree = recipeModel.DairyFree,
                Vegetarian = recipeModel.Vegetarian,
                Vegan = recipeModel.Vegan,
                Nuts = recipeModel.Nuts,
                Instruction = InstructionModel.ModelToDto(recipeModel.Instructions),
                Ingredient = IngredientModel.ModelToDto(recipeModel.Ingredients)
            };            
        }
    }
}