using Microsoft.AspNetCore.Mvc;

namespace SocialRecipes.API.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
