using Budget_CoolBooks.Models;

namespace Budget_CoolBooks.ViewModels
{
    public class QuoteViewModel
    {
        public Quote Quote { get; set; }
        public List<Quote> Quotes { get; set; }
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }

        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public int AuthorId { get; set; }
    }
}
