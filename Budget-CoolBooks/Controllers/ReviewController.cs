using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Budget_CoolBooks.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewServices _ReviewServices;
        private readonly BookServices _bookServices;
        private readonly UserServices _userServices;

        public ReviewController(ReviewServices reviewServices, BookServices bookServices, UserServices userServices)
        {
            _ReviewServices = reviewServices;
            _bookServices = bookServices;
            _userServices = userServices;
        }
       
        public async Task<IActionResult> Index(int id)
        {

            return View();
        }

        // ADD BOOK - GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("~/views/review/Create.cshtml");
        }

            // ADD BOOK - POST
            [HttpPost]
        public async Task<IActionResult> Create(string title, string text, double rating, int bookId)
        {
            // Get Book object to review !!FIX BOOKID FROM DETAILS PAGE!!
            var book = await _bookServices.GetBookById(4);
            if (book == null)
            {
                NotFound();
            }
            
            // Create a Review object and pass in user input.
            Review review = new Review()
            {
                Title = title,
                Text = text,
                Rating = rating,
                Created = DateTime.Now,
                IsDeleted = false,
                Like = 0,
                Dislike = 0,
                Flag = 0,
            };

            // Adds correct book to review.
            review.Book = book;
        

            // Validate the id for user.
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUser == null)
            {
                ModelState.AddModelError("", "Could not find user");
                return StatusCode(500, ModelState);
            }


            if (!await _ReviewServices.CreateReview(review, currentUserID))
            {
                return BadRequest();
            }

            return View();
        }
    }
}
