using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Genres;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Mono.TextTemplating;
using System.Data;
using System.Security.Claims;

namespace Budget_CoolBooks.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ReviewServices _reviewServices;
        private readonly GenreServices _genreServices;
        private readonly AuthorServices _authorServices;
        private readonly UserServices _userServices;
        private readonly BookServices _bookServices;
        private readonly ApplicationDbContext _context;

        public AdminController(ReviewServices reviewServices, ApplicationDbContext context)
        {
            _context = context;
            _reviewServices = reviewServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int totalComments = _context.Comments.ToList().Count;
            int totalBooks = _context.Books.Where(b=>b.IsDeleted == false).ToList().Count;
            int totalUsers = _context.Users.ToList().Count;
            int totalAuthors = _context.Authors.ToList().Count;

            var total = new TotalinfoViewModel()
            {
                TotalComments = totalComments,
                TotalBooks = totalBooks,
                TotalUsers = totalUsers,
                TotalAuthors = totalAuthors,
            };

            return View(total);
        }


        //___________REVIEWS_______________________________________
        [HttpGet]
        public IActionResult AdminReviews()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReviewsByName(string userName)
        {

            var result = await _reviewServices.GetReviewByUsername(userName);
            if (result == null)
            {
                return NotFound();
            }
            ViewBag.Reviews = result;
            return View("AdminReviews", ViewBag.Reviews);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var reviewToDelete = await _reviewServices.GetReviewByID(reviewId);
            if (reviewToDelete == null)
            {
                return NotFound();
            }
            if (!await _reviewServices.DeleteReview(reviewToDelete))
            {
                return NotFound();
            }
            return View("AdminReviews");
        }
    }
}
