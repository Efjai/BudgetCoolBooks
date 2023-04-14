using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Budget_CoolBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookServices _bookServices;

        public HomeController(ILogger<HomeController> logger, BookServices bookServices)
        {
            _logger = logger;
            _bookServices = bookServices;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult ContactUs()
        {
            return View();
        }

        //public IActionResult Bookcard()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _bookServices.GetAllBooksSorted();
            ViewBag.BookList = result;
            return View(ViewBag.BookList);
        }

        [HttpGet]
        public async Task<IActionResult> Bookcard()
        {
            var result = await _bookServices.GetAllBooksSorted();
            ViewBag.BookList = result;
            return View(ViewBag.BookList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}