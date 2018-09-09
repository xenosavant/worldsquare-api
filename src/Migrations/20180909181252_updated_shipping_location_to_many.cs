using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class updated_shipping_location_to_many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PrimaryShippingLocationId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrimaryShippingLocationId",
                table: "AspNetUsers",
                column: "PrimaryShippingLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PrimaryShippingLocationId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrimaryShippingLocationId",
                table: "AspNetUsers",
                column: "PrimaryShippingLocationId",
                unique: true,
                filter: "[PrimaryShippingLocationId] IS NOT NULL");
        }
    }
}
