using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget_CoolBooks.Services.Search
{
    public class SearchServices
    {
        private readonly ApplicationDbContext _context;

        public SearchServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Book>> SearchAll(string search)
        {
            var result = _context
                        .Books
                        .Include(b => b.Author)
                        .Where(b => b.Title.Contains(search) ||
                        b.Author.Firstname.Contains(search) ||
                        b.Author.Lastname.Contains(search) ||
                        b.ISBN.Contains(search) ||
                        b.Author.Books.Any(ab => ab.Author.Firstname.Contains(search) ||
                                                 ab.Author.Lastname.Contains(search)))
                        .ToList();
            return result;
        }
    }
}
