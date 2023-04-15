using Budget_CoolBooks.Models;

namespace Budget_CoolBooks.ViewModels
{
    public class SearchViewModel
    {
        public List<Book> Books { get; set; }
        public List<Book> SortedBooks { get; set; }
        public string SortBy { get; set; }
        public bool SearchActive { get; set; }
        public string OriginalSearchString { get; set; } // Keep modelstate in order to sort result later.
    }
}
