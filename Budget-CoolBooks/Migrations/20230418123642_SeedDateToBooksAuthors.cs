using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_CoolBooks.Migrations
{
    /// <inheritdoc />
    public partial class SeedDateToBooksAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into BooksAuthors(BookID,AuthorId) values (1,1);\r\nInsert into BooksAuthors(BookID,AuthorId) values (2,2);\r\nInsert into BooksAuthors(BookID,AuthorId) values (3,3);\r\nInsert into BooksAuthors(BookID,AuthorId) values (4,4);\r\nInsert into BooksAuthors(BookID,AuthorId) values (5,5);\r\nInsert into BooksAuthors(BookID,AuthorId) values (6,6);\r\nInsert into BooksAuthors(BookID,AuthorId) values (7,5);\r\nInsert into BooksAuthors(BookID,AuthorId) values (8,7);\r\nInsert into BooksAuthors(BookID,AuthorId) values (9,8);\r\nInsert into BooksAuthors(BookID,AuthorId) values (10,9);\r\nInsert into BooksAuthors(BookID,AuthorId) values (11,10);\r\nInsert into BooksAuthors(BookID,AuthorId) values (12,11);\r\nInsert into BooksAuthors(BookID,AuthorId) values (13,12);\r\nInsert into BooksAuthors(BookID,AuthorId) values (14,13);\r\nInsert into BooksAuthors(BookID,AuthorId) values (15,14);\r\nInsert into BooksAuthors(BookID,AuthorId) values (16,1);\r\nInsert into BooksAuthors(BookID,AuthorId) values (17,15);\r\nInsert into BooksAuthors(BookID,AuthorId) values (18,16);\r\nInsert into BooksAuthors(BookID,AuthorId) values (19,1);\r\nInsert into BooksAuthors(BookID,AuthorId) values (20,17);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
