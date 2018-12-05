using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class refactormessagethreads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageThreads_AspNetUsers_InitiatorId",
                table: "MessageThreads");

            migrationBuilder.DropTable(
                name: "ListingMessageThread");

            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "MessageThreads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MessageThreadMember",
                columns: table => new
                {
                    MessageThreadId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ListingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageThreadMember", x => new { x.MessageThreadId, x.UserId });
                    table.ForeignKey(
                        name: "FK_MessageThreadMember_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageThreadMember_MessageThreads_MessageThreadId",
                        column: x => x.MessageThreadId,
                        principalTable: "MessageThreads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageThreadMember_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreads_ListingId",
                table: "MessageThreads",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreadMember_ListingId",
                table: "MessageThreadMember",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreadMember_UserId",
                table: "MessageThreadMember",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThreads_AspNetUsers_InitiatorId",
                table: "MessageThreads",
                column: "InitiatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThreads_Listings_ListingId",
                table: "MessageThreads",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageThreads_AspNetUsers_InitiatorId",
                table: "MessageThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageThreads_Listings_ListingId",
                table: "MessageThreads");

            migrationBuilder.DropTable(
                name: "MessageThreadMember");

            migrationBuilder.DropIndex(
                name: "IX_MessageThreads_ListingId",
                table: "MessageThreads");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "MessageThreads");

            migrationBuilder.CreateTable(
                name: "ListingMessageThread",
                columns: table => new
                {
                    ListingId = table.Column<int>(nullable: false),
                    MessageThreadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingMessageThread", x => new { x.ListingId, x.MessageThreadId });
                    table.ForeignKey(
                        name: "FK_ListingMessageThread_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListingMessageThread_MessageThreads_MessageThreadId",
                        column: x => x.MessageThreadId,
                        principalTable: "MessageThreads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingMessageThread_MessageThreadId",
                table: "ListingMessageThread",
                column: "MessageThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThreads_AspNetUsers_InitiatorId",
                table: "MessageThreads",
                column: "InitiatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
