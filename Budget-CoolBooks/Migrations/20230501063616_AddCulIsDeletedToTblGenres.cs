using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_CoolBooks.Migrations
{
    /// <inheritdoc />
    public partial class AddCulIsDeletedToTblGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Genres",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Genres");
        }
    }
}
