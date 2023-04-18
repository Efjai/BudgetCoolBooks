namespace Budget_CoolBooks.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Created { get; set; }

        //Nested properties
        public List<Book_Author> AuthorList { get; } = new();        

        public Author(string firstname, string lastname, DateTime created)
        {
            Firstname = firstname;
            Lastname = lastname;
            Created = created;
        }
    }
}
