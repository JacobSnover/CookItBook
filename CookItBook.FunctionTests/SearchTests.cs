using CookItBook.Infrastructure;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class SearchTests
    {
        private IRecipeRepository recipeRepo;

        [SetUp]
        public void Setup()
        {
            recipeRepo = new RecipeRepository();
        }

        [Test]
        public void GetRecipesTest()
        {
            List<Recipe> recipeList = new List<Recipe>();
            foreach (Recipe recipe in recipeRepo.GetRecipes())
            {
                recipeList.Add(recipe);
            }

            Assert.IsInstanceOf(typeof(List<Recipe>), recipeList);
            Assert.Positive(recipeList.Count);
            Assert.NotZero(recipeList.Count);
        }
    }
}