using Budget_CoolBooks.Services.Quotes;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Budget_CoolBooks.Controllers
{
    public class QuoteController : Controller
    {
        private readonly QuoteServices _quoteServices;

        public QuoteController(QuoteServices quoteServices)
        {
            _quoteServices = quoteServices;
        }
        public async Task<IActionResult> Index()
        {
            var quotes = await _quoteServices.GetQuotes();
            if(quotes == null)
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
            return View(quoteViewModel);
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
            return View("~/views/quote/index.cshtml", quoteViewModel);
        }
        
    }
}
