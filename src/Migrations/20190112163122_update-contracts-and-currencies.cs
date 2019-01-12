using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class updatecontractsandcurrencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "SecretKeys",
                newName: "ObligationId");

            migrationBuilder.RenameColumn(
                name: "ContractTypeId",
                table: "Contracts",
                newName: "ObligationId");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Listings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Precision",
                table: "Currencies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Obligation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UniqueId = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: true),
                    RecipientId = table.Column<int>(nullable: false),
                    ArbiterId = table.Column<int>(nullable: true),
                    InteracationId = table.Column<int>(nullable: false),
                    ServiceInitiationTimeLimit = table.Column<DateTime>(nullable: false),
                    ServiceFulfillmentTimeLimit = table.Column<DateTime>(nullable: false),
                    ServiceReceiptTimeLimit = table.Column<DateTime>(nullable: false),
                    IntermediaryPhases = table.Column<int>(nullable: false),
                    Fulfilled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obligation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obligation_Interactions_InteracationId",
                        column: x => x.InteracationId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CurrencyId",
                table: "Listings",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ObligationId",
                table: "Contracts",
                column: "ObligationId");

            migrationBuilder.CreateIndex(
                name: "IX_Obligation_InteracationId",
                table: "Obligation",
                column: "InteracationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Obligation_ObligationId",
                table: "Contracts",
                column: "ObligationId",
                principalTable: "Obligation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Currencies_CurrencyId",
                table: "Listings",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Obligation_ObligationId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Currencies_CurrencyId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "Obligation");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CurrencyId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ObligationId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Precision",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "ObligationId",
                table: "SecretKeys",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "ObligationId",
                table: "Contracts",
                newName: "ContractTypeId");
        }
    }
}
