using CookItBook.Controllers;
using CookItBook.Domain;
using CookItBook.Infrastructure;
using CookItBook.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class FlowTests
    {
        private RecipeViewModel recipeViewModel;
        private HomeController homeController;
        private RecipeManager recipeManager;
        private RecipeRepository recipeRepository;
        private readonly RecipeModel recipe = new RecipeModel()
        {
            Name = "2 Ingredient Pizza Dough",
            Ingredients = new List<IngredientModel>()
            {
                new IngredientModel() { Name = "1 1/2 cups self rising flour" },
                new IngredientModel() { Name = "1 cup Greek Yogurt" }
            },
            Instructions = new List<InstructionModel>()
            {
                new InstructionModel() { Step = "Mix ingredients in medium to large mixing bowl, until you can form the dough into a ball." },
                new InstructionModel() { Step = "Find a flat surface good enough to kneade your dough on, and coat the surface with a light dusting of flour." },
                new InstructionModel() { Step = "Take dough from mixing bowl and begin to kneade the dough on the flat surface while making sure" +
                    "there is enough flour on the surface so that the dough will not stick to the surface, adding flour as needed." },
                new InstructionModel() { Step = "Once the dough is able to be formed into shapes, and doesn't stick to your hands." },
                new InstructionModel() { Step = "You can know let the dough rest if you want, but it is not necessary." },
                new InstructionModel() { Step = "Make into shape you prefer, and enjoy!" }
            },
            Vegetarian = true,
            Vegan = false
        };

        [SetUp]
        public void Setup()
        {
            recipeRepository = new RecipeRepository();
            recipeManager = new RecipeManager(recipeRepository);
            getViewModel();
        }

        public void getViewModel()
        {
            recipeViewModel = new RecipeViewModel();

            foreach (Recipe item in recipeManager.GetRecipes())
            {
                recipeViewModel.Recipes.Add(RecipeModel.DtoToModel(item));
            }
        }

        [Test]
        public void SetupTest()
        {
            Assert.IsNotNull(recipeRepository);
            Assert.IsNotNull(recipeManager);
            Assert.IsNotNull(recipeViewModel);
        }

        [Test]
        public void UpdateRecipeTest()
        {
            var index = 1;
            var recipe = new RecipeModel();

            recipe = recipeViewModel.Recipes.Where(r => r.RecipeID == 1).SingleOrDefault();

            Assert.AreEqual(index, recipe.RecipeID);
        }

        [Test]
        public void GetRecipesFromManagerTest()
        {
            recipeViewModel = new RecipeViewModel();
            var recipeList = recipeManager.GetRecipes();

            foreach (var item in recipeList)
            {
                recipeViewModel.Recipes.Add(RecipeModel.DtoToModel(item));
            }

            Assert.IsInstanceOf(typeof(RecipeViewModel), recipeViewModel);
            Assert.Positive(recipeViewModel.Recipes.Count);
            Assert.NotZero(recipeViewModel.Recipes.Count);
        }

        [Test]
        public void AddRecipeFromManagerTest()
        {
            var testRecipeDto = RecipeModel.ModelToDto(recipe);
            var foundTestRecipe = new Recipe();
            Ingredient[] ingArray = new Ingredient[testRecipeDto.Ingredient.Count];                
            Instruction[] insArray = new Instruction[testRecipeDto.Instruction.Count];
            testRecipeDto.Ingredient.CopyTo(ingArray, 0);
            testRecipeDto.Instruction.CopyTo(insArray, 0);


            int recipeID = recipeManager.Add(testRecipeDto);
            foundTestRecipe = recipeManager.Find(recipeID);
            Ingredient[] foundIngArray = new Ingredient[foundTestRecipe.Ingredient.Count];
            Instruction[] foundInsArray = new Instruction[foundTestRecipe.Instruction.Count];
            foundTestRecipe.Ingredient.CopyTo(foundIngArray, 0);
            foundTestRecipe.Instruction.CopyTo(foundInsArray, 0);

            Assert.IsInstanceOf(typeof(Recipe), testRecipeDto);
            Assert.IsInstanceOf(typeof(Recipe), foundTestRecipe);
            Assert.AreEqual(testRecipeDto.RecipeID, foundTestRecipe.RecipeID);
            Assert.AreEqual(testRecipeDto.Name, foundTestRecipe.Name.ToString().Trim());
            Assert.AreEqual(testRecipeDto.GlutenFree, foundTestRecipe.GlutenFree);
            Assert.AreEqual(testRecipeDto.Vegan, foundTestRecipe.Vegan);
            Assert.AreEqual(testRecipeDto.Vegetarian, foundTestRecipe.Vegetarian);
            Assert.AreEqual(testRecipeDto.Nuts, foundTestRecipe.Nuts);
            Assert.AreEqual(testRecipeDto.DairyFree, foundTestRecipe.DairyFree);


            for (int i = 0; i < ingArray.Length; i++)
            {
                Assert.AreEqual(ingArray[i].Value, foundIngArray[i].Value);
            }

            for (int i = 0; i < insArray.Length; i++)
            {
                Assert.AreEqual(insArray[i].Value, foundInsArray[i].Value);
            }

            recipeManager.RollBack(testRecipeDto);

        }
    }
}