﻿using Budget_CoolBooks.Data;
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

        //public async Task<Book> GetFullBookById(int bookId)
        //{
        //    return await _context.Books
        //                .Include(b => b.Author)
        //                .Include(b => b.user)
        //                .Include(b => b.Genre)
        //                .Where(b => !b.IsDeleted)
        //                .FirstOrDefaultAsync(b => b.Id == bookId);
        //}
         

        public async Task<ICollection<Book>> GetBookListByID(int bookId)
        {
            return _context.Books.Where(b => b.Id == bookId && !b.IsDeleted).ToList();
        }

        public async Task<ICollection<Book>> GetAllBooksSorted()
        {
            return _context.Books
                //.Include(b => b.Author)
                .Include(b => b.user)
                //.Include(b => b.Genre)
                .Where(b => !b.IsDeleted)
                .OrderBy(b => b.Title)
                .ToList();
        }

        public async Task<bool> CreateBook(Book book, string userId, int authorId, int genreId)
        {
            var user = await _context.Users.FindAsync(userId); // This must be the admin who is responsible for creating book.
            if (user == null)
            {
                return false;
            }
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null)
            {
                return false;
            }
            var genre = await _context.Genres.FindAsync(genreId);
            if (genre == null)
            {
                return false;
            }
   
            book.user = user;
            _context.Books.Add(book);

            BookAuthor bookAuthor = new BookAuthor();
            bookAuthor.Author = author; bookAuthor.Book = book;
            _context.BooksAuthors.Add(bookAuthor);

            BookGenre bookGenre = new BookGenre();
            bookGenre.Genre = genre; bookGenre.Book = book;
            _context.BooksGenres.Add(bookGenre);

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
