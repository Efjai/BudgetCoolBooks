namespace Budget_CoolBooks.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }
        public int Flag { get; set; }

        //Nested properties 
        public User User { get; set; }
        public Review Review { get; set; }
    }
}
