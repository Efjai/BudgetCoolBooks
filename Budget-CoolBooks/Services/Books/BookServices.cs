using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Net;
using System.Security.Cryptography;

namespace Budget_CoolBooks.Services.Books
{
    public class BookServices
    {
        private readonly ApplicationDbContext _context;

        public BookServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Book> GetBookById(int bookId)
        {
            return _context.Books.Where(b => b.Id == bookId && !b.IsDeleted).FirstOrDefault();
        }

        public async Task<ICollection<Book>> GetBookListByID(int bookId)
        {
            return _context.Books.Where(b => b.Id == bookId && !b.IsDeleted).ToList();
        }
        public async Task<Book> GetFullBookById(int bookId)
        {
            return await _context.Books
                        .Include(b => b.BookAuthor)
                            .ThenInclude(a => a.Author)
                        .Include(b => b.BookGenre)
                            .ThenInclude(c => c.Genre)
                        .Include(b => b.user)
                        .Where(b => !b.IsDeleted)
                        .FirstOrDefaultAsync(b => b.Id == bookId);
        }
        public async Task<ICollection<Book>> GetAllBooksSorted()
        {
            return _context.Books
                .Include(b => b.user)
                .Where(b => !b.IsDeleted)
                .OrderBy(b => b.Title)
                .ToList();
        }

        public async Task<bool> AddBookGenre(BookGenre bookGenre)
        {
            _context.BooksGenres.Add(bookGenre);
            return Save();
        }

        public async Task<bool> AddBookAuthor(BookAuthor bookAuthor)
        {
            _context.BooksAuthors.Add(bookAuthor);
            return Save();
    }

    public async Task<bool> CreateBook(Book book, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            book.user = user;
            _context.Books.Add(book);

            return Save();
        }

        public async Task<bool> UpdateBook(Book book)
        {
            _context.Books.Update(book);
            return Save();
        }


        public async Task<bool> DeleteBook(Book book)
        {
            book.IsDeleted = true;
            var result = _context.Books.Update(book);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
