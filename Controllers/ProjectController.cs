using Microsoft.AspNetCore.Mvc;

namespace LetsChatFinal.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
