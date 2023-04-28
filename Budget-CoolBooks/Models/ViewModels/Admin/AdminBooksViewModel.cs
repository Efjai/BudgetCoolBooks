using Budget_CoolBooks.Models;

namespace Budget_CoolBooks.ViewModels
{
    public class AdminBooksViewModel
    {
        public List<Book> Books { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Author> Authors { get; set; }

        public List<List<Author>> AuthorsList { get; set; }  // Allows access to each author of every book
        public Book Book { get; set; }
    }
}
