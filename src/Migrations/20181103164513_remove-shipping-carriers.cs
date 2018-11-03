using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class removeshippingcarriers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductShipments_ShippingCarriers_ShippingCarrierId",
                table: "ProductShipments");

            migrationBuilder.DropTable(
                name: "ShippingCarriers");

            migrationBuilder.DropIndex(
                name: "IX_ProductShipments_ShippingCarrierId",
                table: "ProductShipments");

            migrationBuilder.DropColumn(
                name: "ShippingCarrierId",
                table: "ProductShipments");

            migrationBuilder.AddColumn<string>(
                name: "ShippingCarrierType",
                table: "ProductShipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingCarrierType",
                table: "ProductShipments");

            migrationBuilder.AddColumn<int>(
                name: "ShippingCarrierId",
                table: "ProductShipments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShippingCarriers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingCarriers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipments_ShippingCarrierId",
                table: "ProductShipments",
                column: "ShippingCarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShipments_ShippingCarriers_ShippingCarrierId",
                table: "ProductShipments",
                column: "ShippingCarrierId",
                principalTable: "ShippingCarriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
