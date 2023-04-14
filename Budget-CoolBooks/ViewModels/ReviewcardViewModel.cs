using Budget_CoolBooks.Models;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Budget_CoolBooks.ViewModels
{
    public class ReviewcardViewModel
    {
        public string ReviewUser { get; set; }
        public DateTime ReviewCreated { get; set; }
        public string ReviewText { get; set; }

    }
}
