using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationComponents",
                table: "Locations",
                newName: "LocationComponentsFromGoogleApi");

            migrationBuilder.AddColumn<string>(
                name: "LocationComponentsFromApp",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationComponentsFromApp",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "LocationComponentsFromGoogleApi",
                table: "Locations",
                newName: "LocationComponents");
        }
    }
}
