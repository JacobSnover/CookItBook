using System.Collections.Generic;
using CookItBook.Infrastructure;

namespace CookItBook.Domain
{
    public class RecipeManager : IRecipeManager
    {
        private IRecipeRepository recipeRepo;

        public RecipeManager(IRecipeRepository recipeRepository)
        {
            recipeRepo = recipeRepository;
        }

        public IEnumerable<Ingredient> GetIngredients()
        {
            return recipeRepo.GetIngredients();
        }

        public IEnumerable<Instruction> GetInstructions()
        {
            return recipeRepo.GetInstructions();
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            return recipeRepo.GetRecipes();
        }

        public int Add(Recipe recipe)
        {
           return recipeRepo.Add(recipe);
        }

        public string Delete(Recipe recipe)
        {
            return recipeRepo.Delete(recipe);
        }

        public Recipe Find(int recipeID)
        {
            return recipeRepo.Find(recipeID);
        }

        public void Update(Recipe recipe)
        {
            recipeRepo.Update(recipe);
        }

        public string RollBack(Recipe recipe)
        {
            return recipeRepo.RollBack(recipe);
        }
    }
}