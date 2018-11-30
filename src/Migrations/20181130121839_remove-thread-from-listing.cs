using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class removethreadfromlisting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_MessageThreads_ThreadId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_ThreadId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "Listings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThreadId",
                table: "Listings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ThreadId",
                table: "Listings",
                column: "ThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_MessageThreads_ThreadId",
                table: "Listings",
                column: "ThreadId",
                principalTable: "MessageThreads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
