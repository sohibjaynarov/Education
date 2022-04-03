using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
