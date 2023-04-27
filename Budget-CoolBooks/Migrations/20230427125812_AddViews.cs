using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_CoolBooks.Migrations
{
    /// <inheritdoc />
    public partial class AddViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE VIEW [dbo].[View_Comments_Authors]\r\nAS\r\nSELECT dbo.Comments.Created, dbo.Authors.Firstname, dbo.Authors.Lastname\r\nFROM   dbo.Comments INNER JOIN\r\n             dbo.Reviews ON dbo.Comments.ReviewId = dbo.Reviews.Id INNER JOIN\r\n             dbo.Books ON dbo.Reviews.BookId = dbo.Books.Id INNER JOIN\r\n             dbo.BooksAuthors ON dbo.Books.Id = dbo.BooksAuthors.BookId INNER JOIN\r\n             dbo.Authors ON dbo.BooksAuthors.AuthorId = dbo.Authors.Id\r\nGO");
            migrationBuilder.Sql("CREATE VIEW [dbo].[View_Comments_Genres]\r\nAS\r\nSELECT dbo.Comments.Created, dbo.Genres.Name\r\nFROM   dbo.Books INNER JOIN\r\n             dbo.BooksGenres ON dbo.Books.Id = dbo.BooksGenres.BookId INNER JOIN\r\n             dbo.Genres ON dbo.BooksGenres.GenreId = dbo.Genres.Id INNER JOIN\r\n             dbo.Reviews ON dbo.Books.Id = dbo.Reviews.BookId INNER JOIN\r\n             dbo.Comments ON dbo.Reviews.Id = dbo.Comments.ReviewId\r\nGO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
