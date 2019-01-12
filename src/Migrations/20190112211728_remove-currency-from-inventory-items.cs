using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class removecurrencyfrominventoryitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_CurrencyAmounts_UnitPriceId",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_UnitPriceId",
                table: "InventoryItems");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "InventoryItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InventoryItemId",
                table: "CurrencyAmounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAmounts_InventoryItemId",
                table: "CurrencyAmounts",
                column: "InventoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyAmounts_InventoryItems_InventoryItemId",
                table: "CurrencyAmounts",
                column: "InventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyAmounts_InventoryItems_InventoryItemId",
                table: "CurrencyAmounts");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyAmounts_InventoryItemId",
                table: "CurrencyAmounts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "InventoryItemId",
                table: "CurrencyAmounts");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_UnitPriceId",
                table: "InventoryItems",
                column: "UnitPriceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_CurrencyAmounts_UnitPriceId",
                table: "InventoryItems",
                column: "UnitPriceId",
                principalTable: "CurrencyAmounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
