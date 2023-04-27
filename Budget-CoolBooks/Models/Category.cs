namespace Budget_CoolBooks.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuoteCategory> QuotesCategories { get; } = new();

    }
}
