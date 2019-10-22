using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Controllers
{
    public class TagsController : Controller
    {
        private readonly RecipeBoxContext _db;

        public TagsController(RecipeBoxContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Tag> model = _db.Tags.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
        return View();
        }

        [HttpPost]
        public ActionResult Create(Tag tag)
        {
        _db.Tags.Add(tag);
        _db.SaveChanges();
        return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
        var thisTag = _db.Tags
            .Include(tag => tag.Recipes)
            .ThenInclude(join => join.Recipe)
            .FirstOrDefault(tag => tag.TagId == id);
        return View(thisTag);
        }

        public ActionResult Edit(int id)
        {
        var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
        return View(thisTag);
        }

        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
        _db.Entry(tag).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
        var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
        return View(thisTag);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
        var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
        _db.Tags.Remove(thisTag);
        _db.SaveChanges();
        return RedirectToAction("Index");
        }


    }
}