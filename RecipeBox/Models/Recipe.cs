using System.Collections.Generic;

namespace RecipeBox.Models
{
    public class Recipe
    {
        public Recipe()
        {
            this.Tags = new HashSet<TagRecipe>();
        }

        public int RecipeId { get; set; }
        public string Instructions { get; set; }
        public virtual ICollection<Recipe> Ingredients { get; set; }
        public ICollection<TagRecipe> Tags { get;}

    

    }
}