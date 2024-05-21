using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthy.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatequantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Food_FoodId",
                table: "Entry");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Entry",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Food_FoodId",
                table: "Entry",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Food_FoodId",
                table: "Entry");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Entry",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

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
