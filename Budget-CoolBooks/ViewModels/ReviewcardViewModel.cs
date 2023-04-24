using Budget_CoolBooks.Models;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Budget_CoolBooks.ViewModels
{
    public class ReviewcardViewModel
    {
        public List<Author> ReviewAuthor { get; set; }
        public List<Book> ReviewBook { get; set; }
        public User ReviewUser { get; set; }
        public List<Review> Review { get; set; }
        public List<Comment> ReviewComment { get; set; }



    }
}
