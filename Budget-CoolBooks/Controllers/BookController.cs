using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Reviews;
using Microsoft.AspNetCore.Mvc;
using Budget_CoolBooks.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace Budget_CoolBooks.Controllers
{
    public class BookController : Controller
    {
        private readonly BookServices _bookServices;
        private readonly AuthorServices _authorServices;
        private readonly ReviewServices _reviewServices;

        public BookController(BookServices bookServices, AuthorServices authorServices, ReviewServices reviewServices)
        {
            _bookServices = bookServices;
            _authorServices = authorServices;
            _reviewServices = reviewServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _bookServices.GetAllBooksSorted();
            if (result == null)
            {
                return NotFound();
            }
            ViewBag.bookListSorted = result;
            return View(ViewBag.bookListSorted);
        }
        [HttpPost]
        public async Task<IActionResult> BookDetails(int id)
        {
            var bookResult = await _bookServices.GetBookById(id);
            if (bookResult == null)
            {
                return NotFound();
            }
            var authorsResult = await _authorServices.GetAuthorsOfBook(id);
            if (authorsResult == null)
            {
                return NotFound();
            }
            var reviewResults = await _reviewServices.GetReviewByID(3);
            if (reviewResults == null)
            {
                return NotFound();
            }

            var bookcardViewModel = new BookcardViewModel()
            {
                BookId = bookResult.Id,
                BookTitle = bookResult.Title,
                BookDescription = bookResult.Description,
                ImgPath = bookResult.Imagepath,

                Authors = authorsResult.ToList(),

                ReviewUser = reviewResults.User,
                ReviewCreated = reviewResults.Created,
                ReviewRating = reviewResults.Rating,
                ReviewTitle = reviewResults.Title,
                ReviewText = reviewResults.Text,
                Like = reviewResults.Like,
                Dislike = reviewResults.Dislike,
                Flag = reviewResults.Flag,
            };
            return View("/views/book/bookcard.cshtml", bookcardViewModel);
        }
    }
}
