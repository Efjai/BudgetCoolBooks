using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Genres;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Budget_CoolBooks.Controllers.Admin
{
    public class AdminBookController : Controller
    {
        private readonly BookServices _bookServices;
        private readonly GenreServices _genreServices;
        private readonly AuthorServices _authorServices;

        public AdminBookController(BookServices bookServices, GenreServices genreServices,
            AuthorServices authorServices)
        {
            _bookServices = bookServices;
            _genreServices = genreServices;
            _authorServices = authorServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookList = await _bookServices.GetAllBooksSorted();

            var adminBookViewModel = new AdminBooksViewModel()
            {
                Books = bookList.ToList(),
            };

            return View("~/views/admin/book/index.cshtml", adminBookViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookServices.GetFullBookById(id);
            var genres = await _genreServices.GetGenres();

            var adminBookViewModel = new AdminBooksViewModel()
            {
                Book = book,
                Genres = genres.ToList(),
            };

            return View("~/views/admin/book/edit.cshtml", adminBookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookServices.GetBookById(id);
            if (result == null)
            {
                return BadRequest(ModelState);
            }

            if (!await _bookServices.DeleteBook(result))
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var genres = await _genreServices.GetGenres();

            var adminBookViewModel = new AdminBooksViewModel()
            {
                Genres = genres.ToList()
            };
            return View("~/views/admin/book/create.cshtml", adminBookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string description, string isbn, string imgpath,
            string authorFirstname, string authorLastname, int genreSelect)
        {
            int authorId;

            // Create book-object
            Book book = new Book(title, description, isbn, imgpath, false, DateTime.Now);

            // See if author exists
            if (!await _authorServices.AuthorExists(authorFirstname, authorLastname))
            {
                //Creates new author if author does not exists
                Author author = new Author(authorFirstname, authorLastname, DateTime.Now);
                if (!await _authorServices.CreateAuthor(author))
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

            if (!await _bookServices.CreateBook(book, currentUserID, authorId, genreSelect))
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }


    }
}
