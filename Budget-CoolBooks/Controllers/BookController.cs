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

        //[HttpPost]
        //public async Task<IActionResult> BookDetails(int id)
        //{
        //    var result = await _bookServices.GetBookListByID(id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.bookList = result;
            
        //    return View("/views/home/bookcard.cshtml", ViewBag.bookList);
        //}

        [HttpPost]
        public async Task<IActionResult> BookDetails(int id)
        {
            var bookResult = await _bookServices.GetFullBookById(id);
            if (bookResult == null)
            {
                return NotFound();
            }

            var bookcardViewModel = new BookcardViewModel()
            {
                BookTitle = bookResult.Title,
                BookDescription = bookResult.Description,
                AuthorFirstname = bookResult.Author.Firstname,
                AuthorLastname = bookResult.Author.Lastname,
                ImgPath = bookResult.Imagepath
            };

            return View("/views/home/bookcard.cshtml", bookcardViewModel);
        }

    }
}
