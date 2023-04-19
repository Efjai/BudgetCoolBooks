using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Authors;
using Microsoft.AspNetCore.Mvc;
using Budget_CoolBooks.ViewModels;

namespace Budget_CoolBooks.Controllers
{
    public class BookController : Controller
    {
        private readonly BookServices _bookServices;
        private readonly AuthorServices _authorServices;

        public BookController(BookServices bookServices, AuthorServices authorServices)
        {
            _bookServices = bookServices;
            _authorServices = authorServices;
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

            var authors = await _authorServices.GetAuthorsOfBook(id);

            var bookcardViewModel = new BookcardViewModel()
            {
                BookTitle = bookResult.Title,
                BookDescription = bookResult.Description,
                ImgPath = bookResult.Imagepath,
                Authors = authors.ToList(),
            };

            return View("/views/book/bookcard.cshtml", bookcardViewModel);
        }
        //[HttpGet]
        //public async Task<IActionResult> bookdetails(int id)
        //{
        //    var bookresult = await _bookServices.GetFullBookById(id);
        //    if (bookresult == null)
        //    {
        //        return NotFound();
        //    }
        //    var bookcardviewmodel = new BookcardViewModel()
        //    {
        //        BookTitle = bookresult.Title,
        //        BookDescription = bookresult.Description,
        //        ImgPath = bookresult.Imagepath,
        //        AuthorFirstname = bookresult.Author.Firstname,
        //        AuthorLastname = bookresult.Lastname,
        //    };
        //    return View("/views/book/bookcard.cshtml", bookcardviewmodel);
        //}
    }
}
