using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget_CoolBooks.Services.Reviews
{
    public class ReviewServices
    {
        private readonly ApplicationDbContext _context;

        public ReviewServices(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<bool> DeleteReview(Review review)
        {
            review.IsDeleted = true;
            var result =_context.Reviews.Update(review);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
