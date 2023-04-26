using Budget_CoolBooks.Models;

namespace Budget_CoolBooks.ViewModels
{
    public class QuoteViewModel
    {
        public Quote Quote { get; set; }
        public List<Quote> Quotes { get; set; }
        public List<Quote> SortedQuotes { get; set; }
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
    }
}
