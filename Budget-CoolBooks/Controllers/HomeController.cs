using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Genres;
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
        private readonly GenreServices _genreServices;

        public HomeController(ILogger<HomeController> logger, BookServices bookServices, SearchServices searchServices, 
            AuthorServices authorServices, GenreServices genreServices)
        {
            _logger = logger;
            _bookServices = bookServices;
            _searchServices = searchServices;
            _authorServices = authorServices;
            _genreServices = genreServices;
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _bookServices.GetAllBooksSorted();            
            var homeViewModel = new HomeViewModel
            {
                Books = result.ToList(),
            };
            return View(homeViewModel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            string cleanedSearchString = "";
            var result = new List<Book>();

            if (string.IsNullOrEmpty(search))
            {
                result = (List<Book>)await _bookServices.GetAllBooksSorted();
            }

            if (search != null)
            {
                cleanedSearchString = search.ToLower();
                result = (List<Book>)await _searchServices.SearchAll(cleanedSearchString);
            }

            var searchViewModel = new SearchViewModel()
            {
                Books = result.ToList(),
                SearchActive = true,
                OriginalSearchString = cleanedSearchString,
                SearchAuthors = (List<Author>)await _authorServices.GetAuthors(),
                SearchGenres = (List<Genre>)await _genreServices.GetGenres(),
            };

            return View("/views/home/search.cshtml", searchViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> SortSearch(int sortInput, string searchString)
        {
            // EJ FIXAD, SKA LÖSA AVERAGE RATING FÖR SORTERING!

            //var listToSort = await _searchServices.SearchAll(searchString);
            //var searchViewModel = new SearchViewModel();

            //if (listToSort == null)
            //{
            //    return BadRequest();
            //}



            //searchViewModel.Books = listToSort.ToList();


            //searchViewModel.OriginalSearchString = searchString;
            //return View("/views/home/search.cshtml", searchViewModel);
            return View("/views/home/search.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> FilterSearch(int genreId, int authorId, string searchString)
        {
            // List of books from search
            var listToFilter = await _searchServices.SearchAll(searchString);

            // If Search was blank, show all books.
            if (string.IsNullOrEmpty(searchString))
            {
                listToFilter = (List<Book>)await _bookServices.GetAllBooksSorted();
            }

            // New list for storing books from search with relevant filter.
            List<Book> filteredBooks = new List<Book>();

            // Creates ViewModel.
            SearchViewModel searchViewModel = new SearchViewModel();

            // Filters if only Author is desired.
            if (authorId != null)
            {
                // Gets reference list of all books by filtered author 
                var compareList = await _authorServices.GetBookListByAuthorId(authorId);

                if (compareList == null)
                {
                    return BadRequest();
                }


                // Compares the searchresults to the reference list
                foreach (Book book in listToFilter)
                {
                    if (compareList.Contains(book))
                    {
                        filteredBooks.Add(book);
                    }
                }

                // Populate ViewModel.
                searchViewModel.OriginalSearchString = searchString;
                searchViewModel.Books = filteredBooks;
                searchViewModel.SearchAuthors = (List<Author>)await _authorServices.GetAuthors();
                searchViewModel.SearchGenres = (List<Genre>)await _genreServices.GetGenres();
                searchViewModel.SearchActive = true;

            }

            // Filters if only Genre is desired.
            if (genreId != null)
            {
                // Gets reference list of all books by filtered genre.
                var compareList = await _bookServices.GetBookListByGenreId(genreId);

                if (compareList == null)
                {
                    return BadRequest();
                }



                foreach (Book book in listToFilter)
                {
                    if (compareList.Contains(book))
                    {
                        filteredBooks.Add(book);
                    }
                }

                // Populate ViewModel.
                searchViewModel.OriginalSearchString = searchString;
                searchViewModel.Books = filteredBooks;
                searchViewModel.SearchAuthors = (List<Author>)await _authorServices.GetAuthors();
                searchViewModel.SearchGenres = (List<Genre>)await _genreServices.GetGenres();
                searchViewModel.SearchActive = true;

            }


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
