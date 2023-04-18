using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_CoolBooks.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataToTblAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Authors(Firstname, Lastname, Created)\r\nvalues ('William', 'Faulkner', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('Mark', 'Twain', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('Lewis', 'Carroll', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('Toni', 'Morrison', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('Charles', 'Dickens', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('Mary', 'Shelley', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('Georg', 'Eliot', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('Virginia', 'Woolf', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('George', 'Orwell', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('Jane', 'Austen', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('J.D', 'Salinger', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('F. Scott', 'Fitzgerald', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('H.G.', 'Wells', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('J.R.R.', 'Tolkien', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('Chinua', 'Achebe', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('Harper', 'Lee', GetDate());\r\nInsert into Authors(Firstname, Lastname, Created)\r\nvalues ('James', 'Joyce', GetDate());");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
