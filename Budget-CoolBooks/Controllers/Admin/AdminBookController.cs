using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Genres;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.Security.Claims;

namespace Budget_CoolBooks.Controllers.Admin
{
    public class AdminBookController : Controller
    {
        private readonly BookServices _bookServices;
        private readonly GenreServices _genreServices;
        private readonly AuthorServices _authorServices;
        private readonly UserServices _userServices;

        public AdminBookController(BookServices bookServices, GenreServices genreServices,
            AuthorServices authorServices, UserServices userServices)
        {
            _bookServices = bookServices;
            _genreServices = genreServices;
            _authorServices = authorServices;
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookList = await _bookServices.GetAllBooksSorted();

            var adminBookViewModel = new AdminBooksViewModel()
            {
                Books = bookList.ToList(),
                // This step creates a identical list from the booklist and populate each item with its related
                // authors in an inner list
                AuthorsList = bookList.Select(b => b.BookAuthor.Select(ba => ba.Author).ToList()).ToList(),
            };

            return View("~/views/admin/book/index.cshtml", adminBookViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookServices.GetBookById(id);

            var adminBookViewModel = new AdminBooksViewModel()
            {
                Book = book,   
            };
          return View("~/views/admin/book/edit.cshtml", adminBookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int bookId, string title, string description,
            string isbn, string imgpath)
        {
            Book book = new Book();
            book = await _bookServices.GetBookById(bookId);
            if (book == null)
            {
                return NotFound();
            }

            if (title != null) { book.Title = title; }
            if (description != null) { book.Description = description; }
            if (isbn != null) { book.ISBN = isbn; }
            if (imgpath != null) { book.Imagepath = imgpath; }

            if (!await _bookServices.UpdateBook(book))
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> AddBookGenre(int id)
        {
            var book = await _bookServices.GetBookById(id);
            var genres = await _genreServices.GetGenres();

            var adminBookViewModel = new AdminBooksViewModel()
            {
                Book = book,
                Genres = genres.ToList(),
            };
          return View("~/views/admin/book/AddBookGenre.cshtml", adminBookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookGenre(int bookId, int genreId)
        {
            var book = await _bookServices.GetBookById(bookId);
            if (book == null)
            {
                return NotFound();
            }

            var genre = await _genreServices.GetGenreById(genreId);
            if(genre == null)
            {
                return NotFound();
            }

            BookGenre bookGenre = new BookGenre();
            bookGenre.Genre = genre; bookGenre.Book = book;

            if (! await _bookServices.AddBookGenre(bookGenre))
            {
                return BadRequest();
            }
            return RedirectToAction("index"); 
        }

        [HttpGet]
        public async Task<IActionResult> AddBookAuthor(int id)
        {
            var book = await _bookServices.GetBookById(id);
            var authors = await _authorServices.GetAuthors();

            var adminBookViewModel = new AdminBooksViewModel()
            {
                Book = book,
                Authors = authors.ToList(),
            };
          return View("~/views/admin/book/AddBookAuthor.cshtml", adminBookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAuthor(int bookId, int authorId)
        {
            var book = await _bookServices.GetBookById(bookId);
            if (book == null)
            {
                return NotFound();
            }

            var author = await _authorServices.GetAuthorById(authorId);
            if (author == null)
            {
                return NotFound();
            }

            BookAuthor bookAuthor = new BookAuthor();
            bookAuthor.Author = author; bookAuthor.Book = book;

            if (!await _bookServices.AddBookAuthor(bookAuthor))
            {
                return BadRequest();
            }
            return RedirectToAction("index");
        }


        // ADD BOOK - GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var authors = await _authorServices.GetAuthors();
            var genres = await _genreServices.GetGenres();

            var adminBookViewModel = new AdminBooksViewModel()
            {
                Authors = authors.ToList(),
                Genres = genres.ToList(),
            };
            return View("~/views/admin/book/create.cshtml", adminBookViewModel);
        }

        // ADD BOOK - POST
        [HttpPost]
        public async Task<IActionResult> Create(string title, string description, string isbn, string imgpath,
            int authorId, int genreId)
        {
            // Create book-object
            Book book = new Book(title, description, isbn, imgpath, false, DateTime.Now);
            
            // Validate and/or procure the id's for author, genre and user.
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUser == null)
            {
                ModelState.AddModelError("", "Could not find user");
                return StatusCode(500, ModelState);
            }

            if (!await _bookServices.CreateBook(book, currentUserID))
            {
                return BadRequest();
            }

            //Add BookGenre-join
            AddBookGenre(book.Id, genreId);

            //Add BookAuthor-join
            AddBookAuthor(book.Id, authorId);

            return RedirectToAction("Index");
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
    }
}
