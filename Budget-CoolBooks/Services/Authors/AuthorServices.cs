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
        public async Task<Author> GetAuthorById(int authorId)
        {
            return _context.Authors.Where(b => b.Id == authorId).FirstOrDefault();
        }

        public async Task<ICollection<Author>> GetAuthors()
        {
            return _context.Authors.ToList();
        }

        public async Task<ICollection<Author>> GetAuthorsOfBook(int bookId)
        {
            return _context.Authors.Where(a => a.BookAuthor.Any(ba => ba.BookId == bookId)).ToList();
        }
        public async Task<ICollection<BookAuthor>> GetBookAuthors()
        {
            return _context.BooksAuthors.ToList();
        }
        public async Task<ICollection<Book>> GetBookListByAuthorId(int authorId)
        {
            return _context.Books.Where(b => b.BookAuthor.Any(ba => ba.AuthorId == authorId) && !b.IsDeleted).ToList();
        }
        public async Task<bool> CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            return Save();
        }
        public async Task<bool> UpdateAuthor(Author author)
        {
            _context.Authors.Update(author);
            return Save();
        }
        public async Task<bool> DeleteAuthor(Author author)
        {
           _context.Authors.Remove(author);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
