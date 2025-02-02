using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksPot.API.Migrations
{
    /// <inheritdoc />
    public partial class IsDeletedPropertyAddedToBookDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SavedBooks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SavedBooks");
        }
    }
}
