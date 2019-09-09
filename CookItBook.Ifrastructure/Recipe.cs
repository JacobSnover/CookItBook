using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookItBook.Infrastructure
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }

        public string Name { get; set; }

        public bool? GlutenFree { get; set; }

        public bool? DairyFree { get; set; }

        public bool? Vegetarian { get; set; }

        public bool? Vegan { get; set; }

        public bool? Nuts { get; set; }

        public ICollection<Ingredient> Ingredient { get; set; }

        public ICollection<Instruction> Instruction { get; set; }

        public Recipe()
        {
            this.Instruction = new List<Instruction>();
            this.Ingredient = new List<Ingredient>();
        }
    }
}