using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Budget_CoolBooks.Services.Reviews
{
    public class ReviewServices
    {
        private readonly ApplicationDbContext _context;

        public ReviewServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateReview(Review review, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            review.User = user;


            _context.Reviews.Add(review);

            return Save();
        }

        public async Task<List<Review>> GetAllReviews()
        {
            return _context.Reviews.Where(r => !r.IsDeleted).OrderByDescending(r => r.Created).ToList();
        }
        public async Task<List<Review>> GetReviewByUsername(string userName)
        {
            // Include navigation-property. Sorts out all username that has IsDeleted=true. Sort by last created.
            return _context.Reviews.Include(r => r.User).Where(r => r.User.UserName == userName && !r.IsDeleted)
                .OrderByDescending(r => r.Created).ToList();
        }
        public async Task<Review> GetReviewByID(int id)
        {
            return _context.Reviews.Where(r => r.Id == id && !r.IsDeleted).FirstOrDefault();
        }
        public async Task<string> CheckForReview(int BookId)
        {
            return _context.Reviews.Where(r => r.Book.Id == BookId).Select(r => r.Title).FirstOrDefault();
        }
        public async Task<bool> DeleteReview(Review review)
        {
            review.IsDeleted = true;
            var result = _context.Reviews.Update(review);
            return Save();
        }
        public async Task<double> GetAverageRating(int bookId)
        {
            // Gives average rating, math.round is not working tho.
            return _context.Reviews.Where(r => r.Book.Id == bookId)
                           .Select(r => r.Rating)
                           .DefaultIfEmpty()
                           .Average(r => Math.Round(r, 1));
        }

        public async Task<Review> GetReviewDetails(int id)
        {
            // Används ej skit.

            return await _context.Reviews
                        .Include(r => r.User)
                        .Include(b => b.Book)
                        .Include(b => b.Book.BookAuthor)
                        .Where(b => !b.Book.IsDeleted)
                        .FirstOrDefaultAsync(b => b.Book.Id == id);

            //return await _context.BooksAuthors
            //            .Include(b => b.Book)
            //            .Include(b => b.Author)
            //            .Where(b => !b.Book.IsDeleted)
            //            .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<ICollection<double>> GetAllRatingsOfBook(int id)
        {
            return _context.Reviews.Where(r => r.Book.Id == id && !r.IsDeleted).Select(r => r.Rating).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task<Review> GetReviewByBookID(int bookId)
        {
            return _context.Reviews.Include(r => r.User).Where(r => r.Book.Id == bookId && !r.IsDeleted).OrderByDescending(r => r.Rating).FirstOrDefault();            
        }
    }
}
