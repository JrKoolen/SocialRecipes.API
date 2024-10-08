using Microsoft.AspNetCore.Mvc;

namespace SocialRecipes.API.Controllers
{
    public class IngredientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
