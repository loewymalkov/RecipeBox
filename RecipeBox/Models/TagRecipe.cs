  
namespace RecipeBox.Models
{
    public class TagRecipe
    {
        public int TagRecipeId { get; set; }
        public int RecipeId { get; set; }
        public int TagId { get; set; }
        public Recipe Recipe { get; set; }
        public Tag Tag { get; set; }
        
    }
}