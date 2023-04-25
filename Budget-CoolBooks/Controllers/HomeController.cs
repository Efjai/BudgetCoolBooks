using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Search;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly AuthorServices _authorServices;

        public HomeController(ILogger<HomeController> logger, BookServices bookServices, SearchServices searchServices, 
            AuthorServices authorServices)
        {
            _logger = logger;
            _bookServices = bookServices;
            _searchServices = searchServices;
            _authorServices = authorServices;
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
                OriginalSearchString = cleanedSearchString,
            };

            return View("/views/home/search.cshtml", searchViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> SortSearch(int sortInput, string searchString)
        {
            var listToSort = await _searchServices.SearchAll(searchString);
            var searchViewModel = new SearchViewModel();

            if (listToSort == null)
            {
                return BadRequest();
            }

            switch (sortInput)
            {
                case 1:
                    var titlesSorted = listToSort
                                       .Where(b => b.Title
                                       .Contains(searchString))
                                       .OrderBy(book => book.Title)
                                       .ToList();

                    searchViewModel.SortedBooks = titlesSorted;
                    break;
                case 2:
                    var authorsSorted = listToSort.SelectMany(b => b.BookAuthor)
                                            .Where(ba => ba.Author.Firstname.Contains(searchString) || ba.Author.Lastname.Contains(searchString))
                                            .Select(ba => ba.Author)
                                            .Distinct()
                                            .ToList();
                    searchViewModel.SortedAuthors = authorsSorted;
                    break;

                case 3:
                    var isbnSorted = listToSort // Sort by ISBN
                                       .Where(b => b.ISBN
                                       .Contains(searchString))
                                       .OrderBy(book => book.ISBN)
                                       .ToList();

                    searchViewModel.SortedISBN = isbnSorted;
                    break;

                default:
                    break;
            }

            searchViewModel.OriginalSearchString = searchString;
            return View("/views/home/search.cshtml", searchViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AuthorDetails(int id)
        {
            var author = await _authorServices.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            AuthorDetailsViewModel authorDetailsViewModel = new AuthorDetailsViewModel()
            {
                Author = author,
            };

            return View("~/views/Home/AuthorDetails.cshtml", authorDetailsViewModel);
        }
    }
}
