using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthy.Data.Migrations
{
    /// <inheritdoc />
    public partial class Entryupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_AspNetUsers_IdentityUserId",
                table: "Entry");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_AspNetUsers_IdentityUserId",
                table: "Entry",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_AspNetUsers_IdentityUserId",
                table: "Entry");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_AspNetUsers_IdentityUserId",
                table: "Entry",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
