using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Search;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Diagnostics;
using static System.Reflection.Metadata.BlobBuilder;

namespace Budget_CoolBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookServices _bookServices;
        private readonly SearchServices _searchServices;

        public HomeController(ILogger<HomeController> logger, BookServices bookServices, SearchServices searchServices)
        {
            _logger = logger;
            _bookServices = bookServices;
            _searchServices = searchServices;
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
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


        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            string cleanedSearchString = search.ToLower();
            var result = await _searchServices.SearchAll(cleanedSearchString);

            var searchViewModel = new SearchViewModel()
            {
                Books = result.ToList(),
                SearchActive = true,
            };

            return View("/views/home/search.cshtml", searchViewModel);
        }

    }
}