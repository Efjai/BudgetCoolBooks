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
        public async Task<bool> UpdateReview(Review review, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            review.User = user;


            _context.Reviews.Update(review);

            return Save();
        }
        public async Task<List<Review>> GetReviewByUserId(string userId)
        {
            // Include navigation-property. Sorts out all username that has IsDeleted=true. Sort by last created.
            return _context.Reviews.Include(r => r.User).Where(r => r.User.Id == userId && !r.IsDeleted).OrderByDescending(r => r.Created).ToList();
        }
        public async Task<Review> GetSpecificReviewByID(int id)
        {
            return _context.Reviews.Include(r => r.User).Where(r => r.Id == id).FirstOrDefault();
        }
        public async Task<List<Review>> GetReviewByUsername(string userName)
        {
            // Include navigation-property. Sorts out all username that has IsDeleted=true. Sort by last created.
            return _context.Reviews.Include(r => r.User).Where(r => r.User.UserName == userName && !r.IsDeleted).OrderByDescending(r => r.Created).ToList();
        }
        public async Task<Review> GetReviewByID(int id)
        {
            // ADDED INCLUDE TO .Include(r => r.Book)
            return _context.Reviews.Include(r => r.User).Where(r => r.Id == id && !r.IsDeleted).FirstOrDefault();
        }
        public async Task<List<Review>> GetReviewListByID(int id)
        {
            // ADDED INCLUDE TO TEST
            return _context.Reviews.Include(r => r.Book).Where(r => r.Id == id && !r.IsDeleted).ToList();
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
        public int GetAverageRating(int bookId)
        {
            // Gives average rating, math.round is not working tho.
            return (int)_context.Reviews.Where(r => r.Book.Id == bookId && !r.IsDeleted).Select(r => r.Rating).DefaultIfEmpty().Average(r => Math.Round((double)r, 1));
        }
        public async Task<ICollection<double>> GetAllRatingsOfBook(int id)
        {
            return _context.Reviews.Where(r => r.Book.Id == id && !r.IsDeleted).Select(r => r.Rating).ToList();
        }
        public async Task<ICollection<Review>> GetFULLAllRatingsOfBook(int id)
        {
            return _context.Reviews.Include(r => r.User).Where(r => r.Book.Id == id && !r.IsDeleted).OrderByDescending(r => r.Like).ToList();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task<Review> GetReviewByBookID(int bookId)
        {
            return _context.Reviews.Include(r => r.User).Where(r => r.Book.Id == bookId && !r.IsDeleted).FirstOrDefault();            
        }
        //Get all ID of reviews by BookID
        public async Task<IList<int>> GetAllIdOfReviews(int id)
        {
            return _context.Reviews.Where(r => r.Book.Id == id && !r.IsDeleted).Select(r => r.Id).ToList();
        }
        public async Task<List<Review>> GetTopRatedBooks()
        {
            return _context.Reviews.Include(r => r.User).Where(r => !r.IsDeleted).OrderByDescending(r => r.Rating).ToList(); 
        }
        public int GetReviewCount(Book book)
        {
            return _context.Reviews.Count(r => r.Book.Id == book.Id && !r.IsDeleted);
        }
    }
}