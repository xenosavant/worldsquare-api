using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class addorderitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicKey",
                table: "Signatures",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "BaseSequenceNumber",
                table: "Contracts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DestAccountId",
                table: "Contracts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceAccountId",
                table: "Contracts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LineItemId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    Fulfilled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_LineItems_LineItemId",
                        column: x => x.LineItemId,
                        principalTable: "LineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_LineItemId",
                table: "OrderItem",
                column: "LineItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropColumn(
                name: "PublicKey",
                table: "Signatures");

            migrationBuilder.DropColumn(
                name: "BaseSequenceNumber",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DestAccountId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SourceAccountId",
                table: "Contracts");
        }
    }
}
