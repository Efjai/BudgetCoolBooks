using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Genres;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
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

        public AdminController(ReviewServices reviewServices, GenreServices genreServices, AuthorServices authorServices
            , UserServices userServices, BookServices bookServices)
        {
            _reviewServices = reviewServices;
            _genreServices = genreServices;
            _authorServices = authorServices;
            _userServices = userServices;
            _bookServices = bookServices;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //BOOKS
        [HttpGet]
        public async Task<IActionResult> AdminBooks()
        {
            var result = await _genreServices.GetGenres();
            if (result == null)
            {
                return NotFound();
            }
            ViewBag.genreList = result;
            return View(ViewBag.genreList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(string title, string description, string isbn, string imgpath, 
            string authorFirstname, string authorLastname, int genreSelect)
        {
            int authorId;

            // Create book-object
            Book book = new Book(title, description, isbn, imgpath, false, DateTime.Now);

            // See if author exists
            if (! await _authorServices.AuthorExists(authorFirstname, authorLastname))
            {
                //Creates new author if author does not exists
                Author author = new Author(authorFirstname, authorLastname, DateTime.Now);
                if(! await _authorServices.CreateAuthor(author))
                {
                    return BadRequest();
                }
                authorId = author.Id;
            }
            else
            {
                authorId = await _authorServices.GetAuthorId(authorFirstname, authorLastname);
            }

            // Validate and/or procure the id's for author, genre and user.
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUser == null)
            {
                ModelState.AddModelError("", "Could not find user");
                return StatusCode(500, ModelState);
            }

            if (!await _genreServices.GenreExists(genreSelect)) 
            {
                return NotFound();
            }
            
            if (! await _bookServices.CreateBook(book, currentUserID, authorId, genreSelect))
            {
                return BadRequest();
            }
            return RedirectToAction("AdminBooks");
        }

        [HttpPost]
        public async Task<IActionResult> ViewBooks()
        {
            var result = await _bookServices.GetAllBooksSorted();
            if (result == null)
            {
                return NotFound();
            }
            ViewBag.bookListSorted = result;
            return View("AdminBooks", ViewBag.bookListSorted);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookServices.GetBookById(id);
            if(result == null)
            {
                return BadRequest(ModelState);
            }

            if (!await _bookServices.DeleteBook(result))
            {
                return NotFound();
            }
            return View("AdminBooks");
        }
        

        //REVIEWS 
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
            if (! await _reviewServices.DeleteReview(reviewToDelete))
            {
                return NotFound();
            }
            return View("AdminReviews");
        }


//________USER-RELATED__________________________________________________

        [HttpGet]
        public async Task<IActionResult> AdminUsers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PromoteUser(string userName)
        {
            var user = await _userServices.GetUserByName(userName);
            if (user == null)
            {
                return NotFound();
            }
            if (!await _userServices.PromoteToAdmin(user))
            {
                ViewBag.AdminAlready = "User is already administrator.";
                return View("AdminUsers", ViewBag.AdminAlready);
            }
            ViewBag.AdminConfirm = userName + " is now added as administrator.";
            return View("AdminUsers", ViewBag.AdminConfirm);
        }


    }
}
