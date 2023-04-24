using Microsoft.AspNetCore.Mvc;

namespace Budget_CoolBooks.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
