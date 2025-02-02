using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksPot.API.Migrations
{
    /// <inheritdoc />
    public partial class SavedBooksDomainChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SavedBooks",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                table: "SavedBooks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "SavedBooks");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SavedBooks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
