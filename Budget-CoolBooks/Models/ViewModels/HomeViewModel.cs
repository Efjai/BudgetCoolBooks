using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Budget_CoolBooks.ViewModels
{
    public class HomeViewModel
    {
        private readonly AuthorServices _authorServices;

        public List<Book> Books { get; set; }        

    }
}
