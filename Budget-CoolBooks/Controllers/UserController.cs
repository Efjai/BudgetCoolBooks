using Microsoft.AspNetCore.Mvc;

namespace Budget_CoolBooks.Controllers
{
    public class User : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
