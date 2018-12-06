using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class removerevieweridfromreviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewerId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ReviewerId",
                table: "Reviews",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                newName: "IX_Reviews_CreatedBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "Reviews",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_CreatedBy",
                table: "Reviews",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_CreatedBy",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Reviews",
                newName: "ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CreatedBy",
                table: "Reviews",
                newName: "IX_Reviews_ReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
