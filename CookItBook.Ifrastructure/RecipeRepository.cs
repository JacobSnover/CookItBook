using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CookItBook.Infrastructure
{
    public class RecipeRepository : IRecipeRepository
    {
        public int Add(Recipe recipe)
        {
            using (var db = new RecipeBook())
            {
                db.Recipe.Add(recipe);
                db.SaveChanges();
                return recipe.RecipeID;
            }
        }

        public Recipe Find(int recipeID)
        {
            var foundRecipe = new Recipe();
            using (var db = new RecipeBook())
            {
                foundRecipe = db.Recipe.Include(x => x.Ingredient).Include(x => x.Instruction).SingleOrDefault(x => x.RecipeID == recipeID);
            }
            return foundRecipe;
        }

        public string RollBack(Recipe recipe)
        {
            var message = new StringBuilder();
            message.Append(Delete(recipe));
            message.Append("due to a RollBack.");
            return message.ToString();
        }

        public string Delete(Recipe recipe)
        {
            var message = $"{recipe.Name} has been removed from the database ";
            using (var db = new RecipeBook())
            {
                db.Remove(recipe);
                db.SaveChanges();
            };
            return message;
        }

        public IEnumerable<Ingredient> GetIngredients()
        {
            using (var db = new RecipeBook())
            {
                return db.Ingredient.ToList();
            }
        }

        public IEnumerable<Instruction> GetInstructions()
        {
            using (var db = new RecipeBook())
            {
                return db.Instruction.ToList();
            }
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            using (var db = new RecipeBook())
            {
                return db.Recipe.Include(x => x.Ingredient).Include(x => x.Instruction).ToList();
            }
        }

        public void Update(Recipe recipe)
        {
            Recipe entity = new Recipe();
            try
            {
                using (var db = new RecipeBook())
                {
                    entity = db.Recipe.Include(x => x.Ingredient).Include(x => x.Instruction).SingleOrDefault(x => x.RecipeID == recipe.RecipeID);

                    if (entity != null)
                    {
                        entity = new Recipe()
                        {
                            RecipeID = recipe.RecipeID,
                            Name = recipe.Name,
                            GlutenFree = recipe.GlutenFree,
                            DairyFree = recipe.DairyFree,
                            Vegetarian = recipe.Vegetarian,
                            Vegan = recipe.Vegan,
                            Nuts = recipe.Nuts,
                            Ingredient = recipe.Ingredient,
                            Instruction = recipe.Instruction
                        };
                        
                        db.Recipe.Update(recipe);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException dbUpdateConCurr)
            {
                foreach (var entry in dbUpdateConCurr.Entries)
                {
                    if (entry.Entity is Recipe)
                    {
                        //var proposedValues = entry.CurrentValues;
                        //var databaseValues = entry.GetDatabaseValues();
                        //foreach (var property in proposedValues.Properties)
                        //{
                        //    var proposedValue = proposedValues[property];
                        //    var databaseValue = databaseValues[property];
                        //    // TODO: decide which value should be written to database
                        //    // proposedValues[property] = <value to be saved>;
                        //}
                        ////Refresh original values to bypass next concurrency check
                        //entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                                               "Don't know how to handle concurrency conflicts for "
                                               + entry.Metadata.Name);
                    }
                }
                //throw new DbUpdateConcurrencyException(dbUpdateConCurr.Message, dbUpdateConCurr.Entries);
            }
            catch (DbUpdateException dbUpdate)
            {
                foreach (var entry in dbUpdate.Entries)
                {
                    if (entry.Entity is Recipe)
                    {
                        //var proposedValues = entry.CurrentValues;
                        //var databaseValues = entry.GetDatabaseValues();

                        //foreach (var property in proposedValues.Properties)
                        //{
                        //    var proposedValue = proposedValues[property];
                        //    var databaseValue = databaseValues[property];

                        //    // TODO: decide which value should be written to database
                        //    // proposedValues[property] = <value to be saved>;
                        //}

                        //// Refresh original values to bypass next concurrency check
                        //entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                                               "Don't know how to handle concurrency conflicts for "
                                               + entry.Metadata.Name);
                    }
                }
            }
        }
    }
}