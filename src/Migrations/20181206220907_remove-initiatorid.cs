using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class removeinitiatorid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageThreads_AspNetUsers_InitiatorId",
                table: "MessageThreads");

            migrationBuilder.DropIndex(
                name: "IX_MessageThreads_InitiatorId",
                table: "MessageThreads");

            migrationBuilder.DropColumn(
                name: "InitiatorId",
                table: "MessageThreads");

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreads_CreatedBy",
                table: "MessageThreads",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThreads_AspNetUsers_CreatedBy",
                table: "MessageThreads",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageThreads_AspNetUsers_CreatedBy",
                table: "MessageThreads");

            migrationBuilder.DropIndex(
                name: "IX_MessageThreads_CreatedBy",
                table: "MessageThreads");

            migrationBuilder.AddColumn<int>(
                name: "InitiatorId",
                table: "MessageThreads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreads_InitiatorId",
                table: "MessageThreads",
                column: "InitiatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThreads_AspNetUsers_InitiatorId",
                table: "MessageThreads",
                column: "InitiatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
