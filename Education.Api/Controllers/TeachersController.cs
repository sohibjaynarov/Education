using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers
{
    public class TeachersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
