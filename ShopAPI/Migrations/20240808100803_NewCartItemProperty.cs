using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewCartItemProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_Carts_CartID",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_items_CartID",
                table: "items");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "items");

            migrationBuilder.DropColumn(
                name: "CartTotal",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "CartItems",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartItems",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CartTotal",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_items_CartID",
                table: "items",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_items_Carts_CartID",
                table: "items",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID");
        }
    }
}
