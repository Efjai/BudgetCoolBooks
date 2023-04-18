using Budget_CoolBooks.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Budget_CoolBooks.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; } 
        public DbSet<Book_Author> Books_Authors { get; set; }
        public DbSet<Book_Genre> Books_Genres { get; set; }
        public DbSet<Comment> Comments { get; set;}
        public DbSet<Reply> Replys { get; set; }
    }
}