using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Models
{
  public class RecipeBoxContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Tag> Tags { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredient{ get; set; }
    public DbSet<TagRecipe> TagRecipe{ get; set; }

    public RecipeBoxContext(DbContextOptions options) : base(options) { }
  }
}