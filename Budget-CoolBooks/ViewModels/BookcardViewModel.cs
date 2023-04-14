using Budget_CoolBooks.Models;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Budget_CoolBooks.ViewModels
{
    public class BookcardViewModel
    {
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string AuthorFirstname { get; set; }
        public string AuthorLastname { get; set; }
        public string ImgPath { get; set; }
    }
}
