namespace Budget_CoolBooks.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set;}
        public bool IsModerated { get; set; }
        public DateTime Created { get; set; }
        public Book? Book { get; set; }
        public Author Author { get; set; }
        public List<QuoteCategory> QuotesCategories { get; } = new();
    }
}
