using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "KycDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "KycDatas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "KycDatas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "KycDatas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "KycDatas");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "KycDatas");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "KycDatas");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "KycDatas");
        }
    }
}
