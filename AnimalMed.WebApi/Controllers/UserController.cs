using Microsoft.AspNetCore.Mvc;

namespace AnimalMed.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
