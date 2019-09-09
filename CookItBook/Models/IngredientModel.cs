using CookItBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookItBook.Models
{
    public class IngredientModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IngredientModel() { }

        public IngredientModel(string name)
        {
            Name = name;
        }

        public IngredientModel(string name, int id)
        {
            Name = name;
            ID = id;
        }


        #region mapper

        public static List<IngredientModel> DtoToModelToList(ICollection<Ingredient> ingredients)
        {
            List<IngredientModel> ingredientList = new List<IngredientModel>();

            foreach (Ingredient ingredient in ingredients)
            {
                ingredientList.Add(new IngredientModel(ingredient.Value, ingredient.Key));
            }

            return ingredientList;
        }

        public static ICollection<Ingredient> ModelToDto(IEnumerable<IngredientModel> ingredients)
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            foreach (var ingredient in ingredients)
            {
                ingredientList.Add(ModelToDto(ingredient));
            }
            return ingredientList;
        }


        public static Ingredient ModelToDto(IngredientModel ingredient)
        {
            return new Ingredient()
            {
                Key = ingredient.ID,
                Value = ingredient.Name.Trim()
            };
        }

        public static ICollection<IngredientModel> ingredientCSVtoDTO(string ingredientCSV)
        {
            List<IngredientModel> ingredientList = new List<IngredientModel>();

            try
            {
                string[] ingredients = ingredientCSV.Split('|');
                foreach (string ingredient in ingredients)
                {
                    if (ingredient != "")
                        ingredientList.Add(new IngredientModel(ingredient.Trim()));
                }

            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine("ingredient" + e.Message);
            }

            return ingredientList;
        }

        #endregion
    }
}
