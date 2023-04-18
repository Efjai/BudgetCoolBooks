using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_CoolBooks.Migrations
{
    /// <inheritdoc />
    public partial class SeedDateToBooksGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into BooksGenres(BookID,GenreId) values (1,1);\r\nInsert into BooksGenres(BookID,GenreId) values (2,1);\r\nInsert into BooksGenres(BookID,GenreId) values (3,7);\r\nInsert into BooksGenres(BookID,GenreId) values (4,1);\r\nInsert into BooksGenres(BookID,GenreId) values (5,1);\r\nInsert into BooksGenres(BookID,GenreId) values (6,3);\r\nInsert into BooksGenres(BookID,GenreId) values (7,1);\r\nInsert into BooksGenres(BookID,GenreId) values (8,1);\r\nInsert into BooksGenres(BookID,GenreId) values (9,1);\r\nInsert into BooksGenres(BookID,GenreId) values (10,4);\r\nInsert into BooksGenres(BookID,GenreId) values (11,1);\r\nInsert into BooksGenres(BookID,GenreId) values (12,1);\r\nInsert into BooksGenres(BookID,GenreId) values (13,1);\r\nInsert into BooksGenres(BookID,GenreId) values (14,1);\r\nInsert into BooksGenres(BookID,GenreId) values (15,2);\r\nInsert into BooksGenres(BookID,GenreId) values (16,1);\r\nInsert into BooksGenres(BookID,GenreId) values (17,1);\r\nInsert into BooksGenres(BookID,GenreId) values (18,4);\r\nInsert into BooksGenres(BookID,GenreId) values (19,1);\r\nInsert into BooksGenres(BookID,GenreId) values (20,1);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
