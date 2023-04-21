using Microsoft.AspNetCore.Identity;

namespace Budget_CoolBooks.Models
{
    public class User : IdentityUser
    {
        public ICollection<Book> Books { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
