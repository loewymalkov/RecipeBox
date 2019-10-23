using System.Collections.Generic;

namespace RecipeBox.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Recipes = new HashSet<TagRecipe>();
           
        }

        public int TagId { get; set; }
        public string TagCategory { get; set; }
        public virtual ICollection<TagRecipe> Recipes { get; set; }
    
    }
}