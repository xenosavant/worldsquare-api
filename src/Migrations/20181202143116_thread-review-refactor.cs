using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class threadreviewrefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnlineStoreReviews");

            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Reviews",
                nullable: true);

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
                name: "IX_Reviews_ListingId",
                table: "Reviews",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ServiceId",
                table: "Reviews",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingMessageThread_MessageThreadId",
                table: "ListingMessageThread",
                column: "MessageThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Listings_ListingId",
                table: "Reviews",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Services_ServiceId",
                table: "Reviews",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Listings_ListingId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Services_ServiceId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "ListingMessageThread");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ListingId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ServiceId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Reviews");

            migrationBuilder.CreateTable(
                name: "OnlineStoreReviews",
                columns: table => new
                {
                    OnlineStoreId = table.Column<int>(nullable: false),
                    ReviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineStoreReviews", x => new { x.OnlineStoreId, x.ReviewId });
                    table.ForeignKey(
                        name: "FK_OnlineStoreReviews_Services_OnlineStoreId",
                        column: x => x.OnlineStoreId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OnlineStoreReviews_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnlineStoreReviews_ReviewId",
                table: "OnlineStoreReviews",
                column: "ReviewId");
        }
    }
}
