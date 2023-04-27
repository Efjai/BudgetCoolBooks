using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Quotes;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using System.Net;

namespace Budget_CoolBooks.Controllers
{
    public class QuoteController : Controller
    {
        private readonly QuoteServices _quoteServices;
        private readonly AuthorServices _authorServices;
        private readonly BookServices _bookServices;

        public QuoteController(QuoteServices quoteServices, AuthorServices authorServices, BookServices bookServices)
        {
            _quoteServices = quoteServices;
            _authorServices = authorServices;
            _bookServices = bookServices;
        }
        public async Task<IActionResult> Index()
        {
            var quotes = await _quoteServices.GetQuotes();
            if (quotes == null)
            {
                return NotFound();
            }

            QuoteViewModel quoteViewModel = new QuoteViewModel();
            quoteViewModel.Quotes = quotes;
            quoteViewModel.Quote = GenerateRandomQuote(quotes);
            return View(quoteViewModel);
        }

        public async Task<IActionResult> ViewAll()
        {
            var quotes = await _quoteServices.GetQuotes();
            if (quotes == null)
            {
                return NotFound();
            }

            var categories = await _quoteServices.GetCategories();
            if (categories == null)
            {
                return NotFound();
            }

            QuoteViewModel quoteViewModel = new QuoteViewModel();
            quoteViewModel.Quotes = quotes;
            quoteViewModel.Categories = categories;

            return View("~/views/quote/viewall.cshtml", quoteViewModel);
        }

        private Quote GenerateRandomQuote(List<Quote> quotes)
        {
            Random rnd = new Random();
            return quotes[rnd.Next(quotes.Count)];
        }

        [HttpPost]
        public async Task<IActionResult> SortQuotes(string sortInput)
        {
            QuoteViewModel quoteViewModel = new QuoteViewModel();
            var sortedQuotes = await _quoteServices.GetSortedQuotes(sortInput);
            if (sortedQuotes == null)
            {
                return NotFound();
            }

            var categories = await _quoteServices.GetCategories();
            if (categories == null)
            {
                return NotFound();
            }

            quoteViewModel.Quotes = sortedQuotes;
            quoteViewModel.Categories = categories;
            return View("~/views/quote/viewall.cshtml", quoteViewModel);
        }


        // CHOOSE AUTHOR
        [HttpGet]
        public async Task<IActionResult> InitialCreate()
        {
            var authors = await _authorServices.GetAuthors();
            if (authors == null)
            {
                return NotFound();
            }

            QuoteViewModel quoteViewModel = new QuoteViewModel();
            quoteViewModel.Authors = authors.ToList();

            return View("~/views/quote/InitialCreate.cshtml", quoteViewModel);
        }

        // AUTHOR GET REGISTERED, PROVIDE BOOKLIST AND CATEGORYLIST TO CHOOSE FROM
        [HttpPost]
        public async Task<IActionResult> InitialCreate(int authorId)
        {
            var books = await _authorServices.GetBookListByAuthorId(authorId);
            if (books == null)
            {
                return NotFound();
            }

            var categories = await _quoteServices.GetCategories();
            if (categories == null)
            {
                return NotFound();
            }

            QuoteViewModel quoteViewModel = new QuoteViewModel();
            quoteViewModel.Books = books.ToList();
            quoteViewModel.AuthorId = authorId;
            quoteViewModel.Categories = categories;

            return View("~/views/quote/create.cshtml", quoteViewModel);
        }


        // Receive full create-input from user
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form, int authorId, int bookId, string quoteText)
        {
            List<Category> categories = new List<Category>();
            var selectedCategories = form["categories"].ToString().Split(',');

            foreach (var categoryName in selectedCategories)
            {
                var categoryObject = await _quoteServices.GetCategoryByName(categoryName);
                if (categoryObject == null)
                {
                    return NotFound();
                }
                categories.Add(categoryObject);
            }

            var author = await _authorServices.GetAuthorById(authorId);
            if (author == null)
            {
                return NotFound();
            }

            //Create quote
            Quote quote = new Quote();
            quote.Text = quoteText;
            quote.Created = DateTime.Now;
            quote.IsModerated = false;
            quote.Author = author;

            // If book exists, add book to quote
            if (bookId != null)
            {
                var book = await _bookServices.GetBookById(bookId);
                if (book == null)
                {
                    return NotFound();
                }
                quote.Book = book;
            }

            //Create quoteCategories
            foreach (var category in categories)
            {
                QuoteCategory quoteCategory = new QuoteCategory();
                quoteCategory.Quote = quote;
                quoteCategory.Category = category;
                if (! await _quoteServices.AddQuoteCategory(quoteCategory))
                {
                    BadRequest();
                }
            }
            return RedirectToAction("Index");
            
        }

    }
}
