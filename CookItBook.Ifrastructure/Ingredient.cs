using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookItBook.Infrastructure
{
    public class Ingredient
    {
        [Key]
        [Column("ID", TypeName = "int")]
        public int Key { get; set; }

        [Column("Value", TypeName = "string")]
        public string Value { get; set; }

        [Column("RecipeID", TypeName = "int")]
        public int RecipeID { get; set; }

        [ForeignKey("RecipeID")]
        public virtual Recipe Recipe { get; set; }

        public Ingredient() { }

        public Ingredient(int key, string value)
        {
            Key = key;
            Value = value;
        }   
    }
}