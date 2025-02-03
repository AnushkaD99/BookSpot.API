using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksPot.API.Migrations
{
    /// <inheritdoc />
    public partial class AddForegKkeytoSavedBookModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SavedBooks_UserId",
                table: "SavedBooks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedBooks_ApplicationUsers_UserId",
                table: "SavedBooks",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedBooks_ApplicationUsers_UserId",
                table: "SavedBooks");

            migrationBuilder.DropIndex(
                name: "IX_SavedBooks_UserId",
                table: "SavedBooks");
        }
    }
}
