
using Microsoft.AspNetCore.Mvc;


namespace RecipeBox.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
