using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class removecontracttypesenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractTypes_ContractTypeId",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractTypeId",
                table: "Contracts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractTypeId",
                table: "Contracts",
                column: "ContractTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractTypes_ContractTypeId",
                table: "Contracts",
                column: "ContractTypeId",
                principalTable: "ContractTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
