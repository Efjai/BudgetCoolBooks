using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
        public async Task<ICollection<Author>> GetAuthorByID(int AuthorId) // TA BORT?
        {
            return _context.Authors.Where(b => b.Id == AuthorId).ToList();
        }

        public async Task<Author> GetAuthorById(int authorId)
        {
            return _context.Authors.Where(b => b.Id == authorId).FirstOrDefault();
        }

        public async Task<ICollection<Author>> GetAuthors()
        {
            return _context.Authors.ToList();
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
