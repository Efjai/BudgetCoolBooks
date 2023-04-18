namespace Budget_CoolBooks.Models
{
    public class Book_Genre
    {
        public int BookId { get; set; }
        public int GenreID { get; set; }

        //Nested properties
        public Book Book { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
    }
}
