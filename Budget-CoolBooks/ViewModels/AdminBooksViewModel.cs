using Budget_CoolBooks.Models;

namespace Budget_CoolBooks.ViewModels
{
    public class AdminBooksViewModel
    {
        public List<Book> Books { get; set; }
        public List<Genre> Genres { get; set; }
        public Book Book { get; set; }
    }
}
