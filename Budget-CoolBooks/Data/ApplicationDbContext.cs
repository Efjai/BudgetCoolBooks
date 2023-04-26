using Budget_CoolBooks.Models;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<BookAuthor> BooksAuthors { get; set; }
        public DbSet<BookGenre> BooksGenres { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replys { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            #region Fluent API

            //Add key to Tbl BookAuthor
            builder.Entity<BookAuthor>()
                .HasKey(b => new { b.AuthorId, b.BookId });


            //Add key to Tbl BookGenres
            builder.Entity<BookGenre>()
                .HasKey(b => new { b.GenreId, b.BookId });


            #endregion
        }
    }
}