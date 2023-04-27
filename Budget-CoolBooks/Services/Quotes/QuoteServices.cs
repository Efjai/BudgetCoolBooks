using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget_CoolBooks.Services.Quotes
{
    public class QuoteServices
    {
        private readonly ApplicationDbContext _context;

        public QuoteServices(ApplicationDbContext context)
        {
            _context = context;
        }


        // QUOTES
        public async Task<Quote> GetQuoteById(int quoteId)
        {
            return _context.Quotes.Where(q => q.Id == quoteId && !q.IsModerated).FirstOrDefault();
        }
        
        public async Task<List<Quote>> GetQuotes()
        {
            return _context.Quotes
                    .Include(q => q.Book)
                    .Include(q => q.Author)
                    .Where(q => q.IsModerated)
                    .OrderBy(q => q.Author.Lastname)
                    .ToList();
        }
        public async Task<List<Quote>> GetSortedQuotes(string sortedInput)
        {
            return _context.Quotes
                    .Include(q => q.Book)
                    .Include(q => q.Author)
                    .Where(q => q.QuotesCategories
                        .Any(qc => qc.Category.Name == sortedInput && q.IsModerated))
                    .ToList();
        }

        public async Task<bool> AddQuote(Quote quote)
        {
            _context.Quotes.Add(quote);
            return Save();
        }


        // CATEGORIES
        public async Task<List<Category>> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return _context.Categories.Where(c => c.Name == categoryName).FirstOrDefault();
        }


        // QUOTE-CATEGORIES
        public async Task<bool> AddQuoteCategory(QuoteCategory quoteCategory)
        {
            _context.QuotesCategories.Add(quoteCategory);
            return Save();
        }

        //OTHER
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
   
}
