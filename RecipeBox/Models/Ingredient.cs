using System.Collections.Generic;

namespace RecipeBox.Models
{
    public class Ingredient

    {
        public Ingredient()
        {
            this.Recipes  = new HashSet<RecipeIngredient>();
            
        }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        // public string Quantity { get; set; }
        public ICollection<RecipeIngredient> Recipes { get;}
    }
}
