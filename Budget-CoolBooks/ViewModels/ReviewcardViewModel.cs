using Budget_CoolBooks.Models;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Budget_CoolBooks.ViewModels
{
    public class ReviewcardViewModel
    {
        public int BookId { get; set; }
        public string ReviewBook { get; set; }
        //public string ReviewAuthor { get; set; }
        public User ReviewUser { get; set; }
        public DateTime ReviewCreated { get; set; }
        public double ReviewRating { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewText { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Flag { get; set; }



    }
}
