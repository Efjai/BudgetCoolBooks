using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Budget_CoolBooks.Controllers.Admin
{
    public class AdminAuthorController : Controller
    {
        private readonly AuthorServices _authorServices;
        private readonly UserServices _userServices;
        private readonly BookServices _bookServices;

        public AdminAuthorController(AuthorServices authorServices, UserServices userServices, BookServices bookServices)
        {
            _authorServices = authorServices;
            _userServices = userServices;
            _bookServices = bookServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authorList = await _authorServices.GetAuthors();

            var adminAuthorViewModel = new AdminAuthorViewModel()
            {
                Authors = authorList.ToList(),
            };


            return View("~/views/admin/author/index.cshtml", adminAuthorViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("~/views/admin/author/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string firstname, string lastname)
        {
            Author author = new Author();
            author.Firstname= firstname;
            author.Lastname= lastname;
            author.Created = DateTime.Now;

            if(!await _authorServices.CreateAuthor(author))
            {
                return BadRequest();
            }

            TempData["message"] = "The author was added successfully";

            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorServices.GetAuthorById(id);
            if (author == null) 
            { 
                return NotFound();
            }

            var adminAuthorViewModel = new AdminAuthorViewModel()
            {
                Author = author
            };
            return View("~/views/admin/author/Edit.cshtml", adminAuthorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string firstname, string lastname, int authorId)
        {
            var author = await _authorServices.GetAuthorById(authorId);
            if(author == null)
            {
                return NotFound();
            }

            author.Firstname = firstname;
            author.Lastname = lastname;

            if (!await _authorServices.UpdateAuthor(author))
            {
                return BadRequest();
            }

            TempData["message"] = "The author information was updated successfully";

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorServices.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            var booksByAuthor = await _authorServices.GetBookListByAuthorId(id);
            // Delete all related books of author if author has authored one book or more.
            if (booksByAuthor == null)
            {
                return NotFound();
            }

            if (booksByAuthor.Count > 0)
            {
                foreach (var book in booksByAuthor)
                {
                    if (!await _bookServices.DeleteBook(book))
                    {
                        return BadRequest();
                    }
                }
            }

            // Delete author
            if (!await _authorServices.DeleteAuthor(author))
            {
                return BadRequest();
            }

            TempData["message"] = "The author and all related books was removed successfully";
            return RedirectToAction("index");
        }
    }
}
