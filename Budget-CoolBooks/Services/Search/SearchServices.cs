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
                        .Include(b =>  b.BookAuthor)
                            .ThenInclude(ba => ba.Author)
                        .Where(b => b.Title.Contains(search) ||
                        b.BookAuthor.Any(ba => ba.Author.Firstname.Contains(search) ||
                                         ba.Author.Lastname.Contains(search)) ||
                                         b.ISBN.Contains(search))
                        .ToList();

            var result2 = _context.Books.Include(b => b.BookGenre).ThenInclude(ba => ba.Genre).Where(b => b.BookGenre.Any(ba => ba.Genre.Name.Contains(search))).ToList();

            foreach (var book in result2)
            {
                result.Add(book);
            }
            return result;
        }
    }
}
