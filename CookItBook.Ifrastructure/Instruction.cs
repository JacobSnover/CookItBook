using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookItBook.Infrastructure
{
    public class Instruction
    {
        [Key]
        [Column("ID", TypeName = "int")]
        public int Key { get; set; }

        [Column("Step", TypeName = "string")]
        public string Value { get; set; }

        [Column("RecipeID", TypeName = "int")]
        public int RecipeID { get; set; }

        [ForeignKey("RecipeID")]
        public virtual Recipe Recipe { get; set; }

        public Instruction() { }

        public Instruction(int key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}