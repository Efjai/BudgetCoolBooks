using System.CodeDom;

namespace Budget_CoolBooks.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Imagepath { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }

        //Nested properties
        //public List<BookGenre> GenreList { get; } = new();
        public List<BookAuthor> BookAuthor { get; } = new();
        public List<BookGenre> BookGenre { get; } = new();
        public User user { get; set; }

        public Book(string title, string description, string iSBN, string imagepath, bool isDeleted, DateTime created)
        {
            Title = title;
            Description = description;
            ISBN = iSBN;
            Imagepath = imagepath;
            IsDeleted = isDeleted;
            Created = created;
        }

        public Book() { }
    }
}
