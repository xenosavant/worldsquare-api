using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class trackingandsignatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractSecretKeys");

            migrationBuilder.AddColumn<string>(
                name: "BuyerSecretKey",
                table: "ProductShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageSecretKey",
                table: "ProductShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingNumber",
                table: "ProductShipments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SecretKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UniqueId = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecretKeys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentTrackers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SecretSigningKey = table.Column<string>(nullable: true),
                    TrackingId = table.Column<string>(nullable: true),
                    TransactionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentTrackers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentTrackers_PreTransactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "PreTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecretKeys_UserId",
                table: "SecretKeys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentTrackers_TransactionId",
                table: "ShipmentTrackers",
                column: "TransactionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecretKeys");

            migrationBuilder.DropTable(
                name: "ShipmentTrackers");

            migrationBuilder.DropColumn(
                name: "BuyerSecretKey",
                table: "ProductShipments");

            migrationBuilder.DropColumn(
                name: "PackageSecretKey",
                table: "ProductShipments");

            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                table: "ProductShipments");

            migrationBuilder.CreateTable(
                name: "ContractSecretKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SecretKey = table.Column<string>(nullable: true),
                    UniqueId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractSecretKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractSecretKeys_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractSecretKeys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractSecretKeys_ContractId",
                table: "ContractSecretKeys",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractSecretKeys_UserId",
                table: "ContractSecretKeys",
                column: "UserId");
        }
    }
}
