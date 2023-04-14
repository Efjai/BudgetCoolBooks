using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace Budget_CoolBooks.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewServices _ReviewServices;

        public ReviewController(ReviewServices reviewServices)
        {
            _ReviewServices = reviewServices;
            
        }
        public async Task<IActionResult> Index()
        {
            var result = await _ReviewServices.GetAllReviews();
            if (result == null)
            {
                return NotFound();
            }
            ViewBag.reviewList = result;
            return View(ViewBag.reviewList);
        }
    }
}
