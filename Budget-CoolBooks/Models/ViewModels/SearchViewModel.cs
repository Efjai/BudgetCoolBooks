using Budget_CoolBooks.Models;

namespace Budget_CoolBooks.ViewModels
{
    public class SearchViewModel
    {
        public List<Book> Books { get; set; }
        public List<Author> SearchAuthors { get; set; }
        public List<Genre> SearchGenres { get; set; }
        public bool SearchActive { get; set; }
        public string OriginalSearchString { get; set; } // Keep modelstate in order to sort result later.

        public Dictionary<string, int> RatingPerBook {  get; set; }

    }
}
