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
            var result = await _bookServices.GetBookById(id);
            if (result == null)
            {
                return NotFound();
            }

            // HÄRIFRÅN KAN DU FORTSÄTTA SOM DU VILL DAVID 
            // Hitta det tack
            ViewBag.bookID = result;
            return View("~/Home/Bookcard", ViewBag.bookID);
        }
        
    }
}
