using CookItBook.Infrastructure;
using System.Collections.Generic;

namespace CookItBook.Domain
{
    public interface IRecipeManager
    {
        IEnumerable<Ingredient> GetIngredients();
        IEnumerable<Instruction> GetInstructions();
        IEnumerable<Recipe> GetRecipes();
        int Add(Recipe recipe);
        Recipe Find(int recipeID);
        void Update(Recipe recipe);
        string Delete(Recipe recipe);
        string RollBack(Recipe recipe);
    }
}