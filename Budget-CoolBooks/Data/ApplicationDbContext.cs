﻿using Budget_CoolBooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteCategory> QuotesCategories { get; set; }

        // Set Views in DB
        public DbSet<CommentsGenresFromView> CommentsGenresFromViews { get; set; }
        public DbSet<CommentsAuthorsFromView> CommentsAuthorsFromViews { get; set; }

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

            //Add key to Tbl QuotesCategories
            builder.Entity<QuoteCategory>()
                .HasKey(ba => new { ba.QuoteId, ba.CategoryId });

            //CommentsGenresFromView is View and Has Not Key
            builder.Entity<CommentsGenresFromView>().HasNoKey().ToView("View_Comments_Genres");

            //CommentsAuthorsFromView is View and Has Not Key
            builder.Entity<CommentsAuthorsFromView>().HasNoKey().ToView("View_Comments_Authors");

            #endregion
        }
    }
}