namespace Budget_CoolBooks.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }
        public int Flag { get; set; }

        //Nested properties 
        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}