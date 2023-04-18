using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_CoolBooks.Migrations
{
    /// <inheritdoc />
    public partial class SeedDateToGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Genres(Name, Description, Created)\r\nvalues ('Roman','', GetDate());\r\n\r\nInsert into Genres(Name, Description, Created)\r\nvalues ('Fantasy','', GetDate());\r\n\r\nInsert into Genres(Name, Description, Created)\r\nvalues ('Horror','', GetDate());\r\n\r\nInsert into Genres(Name, Description, Created)\r\nvalues ('Science Fiction','', GetDate());\r\n\r\nInsert into Genres(Name, Description, Created)\r\nvalues ('Thriller','', GetDate());\r\n\r\nInsert into Genres(Name, Description, Created)\r\nvalues ('Biography','', GetDate());\r\n\r\nInsert into Genres(Name, Description, Created)\r\nvalues ('Childrens','', GetDate());");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
