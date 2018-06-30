using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class Kyc_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KycDatas_AspNetUsers_UserId",
                table: "KycDatas");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "KycDatas",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KycDatas_AspNetUsers_UserId",
                table: "KycDatas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KycDatas_AspNetUsers_UserId",
                table: "KycDatas");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "KycDatas",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_KycDatas_AspNetUsers_UserId",
                table: "KycDatas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
