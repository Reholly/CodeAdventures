using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class editUserAgaion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_IdentityUserId",
                table: "ApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_IdentityUserId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "ApplicationUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "ApplicationUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_IdentityUserId",
                table: "ApplicationUsers",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_IdentityUserId",
                table: "ApplicationUsers",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
