using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_CoolBooks.Migrations
{
    /// <inheritdoc />
    public partial class TotalFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalFlags",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalFlags",
                table: "AspNetUsers");
        }
    }
}
