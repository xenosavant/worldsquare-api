using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class kyc3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KycDatas_Countries_CountryId",
                table: "KycDatas");

            migrationBuilder.DropIndex(
                name: "IX_KycDatas_CountryId",
                table: "KycDatas");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "KycDatas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "KycDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_KycDatas_CountryId",
                table: "KycDatas",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_KycDatas_Countries_CountryId",
                table: "KycDatas",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
