namespace Budget_CoolBooks.Models
{
    public class QuoteCategory
    {
        public int QuoteId { get; set; }
        public int CategoryId { get; set; }
        public Quote Quote { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
