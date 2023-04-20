using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Budget_CoolBooks.ViewModels
{
    public class BookcardViewModel
    {
        private readonly AuthorServices _authorServices;

        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public List<Author> Authors { get; set; }
        public string ImgPath { get; set; }
        public User ReviewUser { get; set; }
        public DateTime ReviewCreated { get; set; }
        public double ReviewRating { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewText { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Flag { get; set; }

        public List<int> RatingsByValue { get; set; }
        public double AverageRating { get; set; }
    }
}
