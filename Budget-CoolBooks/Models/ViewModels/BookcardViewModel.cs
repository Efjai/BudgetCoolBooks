using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Budget_CoolBooks.ViewModels
{
    public class BookcardViewModel
    {
        private readonly AuthorServices _authorServices;

        public List<Book> Books { get; set; }
        public List<Author>? Authors { get; internal set; }
        public List<int>? RatingsByValue { get; set; }
        public double? AverageRating { get; set; }
        public int? IsNotReviewed { get; set; }
        public List<Review>? AllFullReviews { get; set; }
        public List<Comment>? CommentsList { get; set; }
        public List<Reply>? RepliesList { get; set; }
        public string CurrentUserId { get; set; }
        
    }
}
