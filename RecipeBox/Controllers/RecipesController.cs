using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly RecipeBoxContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
        {
        _userManager = userManager;
        _db = db;
        }

        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id);
            return View(userRecipes);
        }

        public ActionResult Create()
        {
        ViewBag.TagId = new SelectList(_db.Tags, "TagId", "TagCategory");
        return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Recipe recipe, int TagId, int IngredientId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            recipe.User = currentUser;
            _db.Recipes.Add(recipe);
            if (TagId != 0)
            {
                _db.TagRecipe.Add(new TagRecipe() { TagId = TagId, RecipeId = recipe.RecipeId });
            }
            if (IngredientId != 0)
            {
                _db.RecipeIngredient.Add(new RecipeIngredient() { IngredientId = IngredientId, RecipeId = recipe.RecipeId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
        var thisRecipe = _db.Recipes
            .Include(recipe => recipe.Tags)
            .ThenInclude(join => join.Tag)
            .Include(recipe => recipe.Ingredients)
            .ThenInclude(join => join.Ingredient)
            .FirstOrDefault(recipe => recipe.RecipeId == id);
        return View(thisRecipe);
        }

        public ActionResult Edit(int id)
        {
            var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
            ViewBag.TagId = new SelectList(_db.Tags, "TagId", "TagCategory");
            ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
            return View(thisRecipe);
        }

        [HttpPost]
        public ActionResult Edit(Recipe recipe, int TagId, int IngredientId)
        {
            if (TagId != 0)
            {
                _db.TagRecipe.Add(new TagRecipe() { TagId = TagId, RecipeId = recipe.RecipeId });
            }
            if (IngredientId != 0)
            {
                _db.RecipeIngredient.Add(new RecipeIngredient() { IngredientId = IngredientId, RecipeId = recipe.RecipeId });
            }
            _db.Entry(recipe).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddTag(int id)
        {
            var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
            ViewBag.TagId = new SelectList(_db.Tags, "TagId", "TagCategory");
            return View(thisRecipe);
        }

        [HttpPost]
        public ActionResult AddTag(Recipe recipe, int TagId)
        {
            if (TagId != 0)
            {
            _db.TagRecipe.Add(new TagRecipe() { TagId = TagId, RecipeId = recipe.RecipeId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddIngredient(int id)
        {
            var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
            ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
            return View(thisRecipe);
        }

        [HttpPost]
        public ActionResult AddIngredient(Recipe recipe, int IngredientId)
        {
            if (IngredientId != 0)
            {
            _db.RecipeIngredient.Add(new RecipeIngredient() { IngredientId = IngredientId, RecipeId = recipe.RecipeId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
            return View(thisRecipe);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
            _db.Recipes.Remove(thisRecipe);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteTag(int joinId)
        {
            var joinEntry = _db.TagRecipe.FirstOrDefault(entry => entry.TagRecipeId == joinId);
            _db.TagRecipe.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteIngredient(int joinId)
        {
            var joinEntry = _db.RecipeIngredient.FirstOrDefault(entry => entry.RecipeIngredientId == joinId);
            _db.RecipeIngredient.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> SortByRating()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userList = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id);
            var userRecipes = userList.OrderBy(order => order.Rating);
            return View("Index", userRecipes);
        }
    }
}