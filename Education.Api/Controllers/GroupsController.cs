using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers
{
    public class GroupsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
