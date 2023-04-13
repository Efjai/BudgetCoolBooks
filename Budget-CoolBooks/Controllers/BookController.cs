using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Books;
using Microsoft.AspNetCore.Mvc;

namespace Budget_CoolBooks.Controllers
{
    public class BookController : Controller
    {
        private readonly BookServices _bookServices;

        public BookController(BookServices bookServices)
        {
            _bookServices = bookServices;
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
            var result = await _bookServices.GetBookListByID(id);
            if (result == null)
            {
                return NotFound();
            }

            ViewBag.book = result;
            return View("/views/home/bookcard.cshtml", ViewBag.book);

            //var result = await _bookServices.GetAllBooksSorted();
            //ViewBag.BookList = result;
            //return View("Views/Home/Bookcard.cshtml", ViewBag.BookList);
        }
        
    }
}
