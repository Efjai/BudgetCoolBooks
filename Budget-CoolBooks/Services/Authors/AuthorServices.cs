using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget_CoolBooks.Services.Authors
{
    public class AuthorServices
    {
        private readonly ApplicationDbContext _context;

        public AuthorServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AuthorExists(string firstName, string lastName)
        {
            return await _context.Authors.AnyAsync(a => a.Firstname == firstName && a.Lastname == lastName);
        }
        public async Task<int> GetAuthorId(string firstName, string lastName)
        {
            var author = await _context.Authors.Where(a => a.Firstname == firstName && a.Lastname == lastName).FirstOrDefaultAsync();
            return author.Id;
        }

        public async Task<bool> CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
