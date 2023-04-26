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

        public async Task<List<Quote>> GetQuotes()
        {
            return _context.Quotes
                    .Include(q => q.Book)
                    .Include(q => q.Author)
                    .OrderBy(q => q.Author.Lastname)
                    .ToList();
        }

        public async Task<List<Quote>> GetSortedQuotes(string sortedInput)
        {
            return _context.Quotes
                    .Include(q => q.Book)
                    .Include(q => q.Author)
                    .Where(q => q.QuotesCategories
                    .Any(qc => qc.Category.Name == sortedInput))
                    .ToList();
        }


        public async Task<List<Category>> GetCategories()
        {
            return _context.Categories.ToList();
        }
    }
   
}
