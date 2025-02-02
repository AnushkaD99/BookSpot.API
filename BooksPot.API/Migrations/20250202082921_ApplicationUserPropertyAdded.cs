using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksPot.API.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserPropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "ApplicationUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ApplicationUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ApplicationUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "ApplicationUsers",
                newName: "FullName");
        }
    }
}
