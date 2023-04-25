using Budget_CoolBooks.Models;

namespace Budget_CoolBooks.ViewModels
{
    public class AuthorDetailsViewModel
    {
        public Author Author { get; set; }
        public List<Book> Books { get; set; }
    }
}
