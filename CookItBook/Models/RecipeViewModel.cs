using CookItBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookItBook.Models
{
    public class RecipeViewModel
    {
        public List<RecipeModel> Recipes { get; set; }

        public RecipeViewModel()
        {
            Recipes = new List<RecipeModel>();
        }
    }
}
