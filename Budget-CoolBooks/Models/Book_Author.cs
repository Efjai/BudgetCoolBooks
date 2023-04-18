﻿namespace Budget_CoolBooks.Models
{
    public class Book_Author
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        //Nested properties
        public Book Book { get; set; } = null!;
        public Author Author { get; set; } = null!;
    }
}
