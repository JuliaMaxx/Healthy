using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthy.Data.Migrations
{
    public partial class cascadedeletion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Food_FoodId",
                table: "Entry");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Food_FoodId",
                table: "Entry",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Food_FoodId",
                table: "Entry");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Food_FoodId",
                table: "Entry",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }

}
