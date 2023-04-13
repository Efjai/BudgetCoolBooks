namespace Budget_CoolBooks.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }

        //Nested properties
        public User User { get; set; }
        public Author Author { get; set; }
    }
}
