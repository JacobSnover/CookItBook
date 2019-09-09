using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CookItBook.Models;
using CookItBook.Domain;
using System;

namespace CookItBook.Controllers
{
    public class HomeController : Controller
    {
        private RecipeModel recipeModel;
        private RecipeViewModel recipeViewModel;
        private IRecipeManager recipeManager;

        public HomeController(IRecipeManager manager)
        {
            recipeModel = new RecipeModel();
            recipeManager = manager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RecipeIndex(int recipeID)
        {
            recipeViewModel = new RecipeViewModel();
            var recipeList = recipeManager.GetRecipes();

            foreach (var item in recipeList)
            {
                recipeViewModel.Recipes.Add(RecipeModel.DtoToModel(item));
            }
            
            return View(recipeViewModel);
        }

        public IActionResult AddRecipe(RecipeModel recipe, string ingredientCSV, string instructionCSV)
        {
            recipe.Ingredients = IngredientModel.ingredientCSVtoDTO(ingredientCSV).ToList();
            recipe.Instructions = InstructionModel.instructionCSVtoDTO(instructionCSV).ToList();

            recipeManager.Add(RecipeModel.ModelToDto(recipe));

            return RedirectToAction("RecipeIndex");
        }
        
        public IActionResult AddRecipeView()
        {
            return View(); 
        }

        public IActionResult UpdateRecipe(RecipeModel recipe, string ingredientCSV, string instructionCSV)
        { 
            recipe.Ingredients = IngredientModel.ingredientCSVtoDTO(ingredientCSV).ToList();
            recipe.Instructions = InstructionModel.instructionCSVtoDTO(instructionCSV).ToList();

            recipeManager.Update(RecipeModel.ModelToDto(recipe));

            return RedirectToAction("RecipeIndex");
        }

        public IActionResult UpdateRecipeView(int RecipeID)
        {
            recipeModel = RecipeModel.DtoToModel(recipeManager.GetRecipes().Where(r => r.RecipeID == RecipeID).SingleOrDefault());
            return View(recipeModel);
        }

        #region MVC default methods
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
