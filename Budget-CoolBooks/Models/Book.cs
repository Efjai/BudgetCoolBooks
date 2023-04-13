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
        public ICollection<Review> Reviews { get; set; }
        public Author Author { get; set; }
        public User user { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }

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
