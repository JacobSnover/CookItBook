using System.Collections.Generic;

namespace CookItBook.Infrastructure
{
    public interface IRecipeRepository
    {
        IEnumerable<Ingredient> GetIngredients();
        IEnumerable<Instruction> GetInstructions();
        IEnumerable<Recipe> GetRecipes();
        Recipe Find(int recipeID);
        int Add(Recipe recipe);
        void Update(Recipe recipe);
        string Delete(Recipe recipe);
        string RollBack(Recipe recipe);
    }
}