using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class addorderorderitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_PosterId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Interactions_OnlineSaleId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Contracts_ContractId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_InventoryItems_InventoryItemId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductShipments_ProductShipmentId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_TradeItems_TradeItemId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductShipments_Order_OrderId",
                table: "ProductShipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameColumn(
                name: "PosterId",
                table: "Messages",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "PostedOn",
                table: "Messages",
                newName: "CreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_PosterId",
                table: "Messages",
                newName: "IX_Messages_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_TradeItemId",
                table: "OrderItems",
                newName: "IX_OrderItems_TradeItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ProductShipmentId",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductShipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_InventoryItemId",
                table: "OrderItems",
                newName: "IX_OrderItems_InventoryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ContractId",
                table: "OrderItems",
                newName: "IX_OrderItems_ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OnlineSaleId",
                table: "Orders",
                newName: "IX_Orders_OnlineSaleId");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "Messages",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "PurchaserId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PurchaserId",
                table: "Orders",
                column: "PurchaserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_CreatedBy",
                table: "Messages",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Contracts_ContractId",
                table: "OrderItems",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_InventoryItems_InventoryItemId",
                table: "OrderItems",
                column: "InventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductShipments_ProductShipmentId",
                table: "OrderItems",
                column: "ProductShipmentId",
                principalTable: "ProductShipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_TradeItems_TradeItemId",
                table: "OrderItems",
                column: "TradeItemId",
                principalTable: "TradeItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Interactions_OnlineSaleId",
                table: "Orders",
                column: "OnlineSaleId",
                principalTable: "Interactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_PurchaserId",
                table: "Orders",
                column: "PurchaserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShipments_Orders_OrderId",
                table: "ProductShipments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_CreatedBy",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Contracts_ContractId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_InventoryItems_InventoryItemId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductShipments_ProductShipmentId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_TradeItems_TradeItemId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Interactions_OnlineSaleId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_PurchaserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductShipments_Orders_OrderId",
                table: "ProductShipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PurchaserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PurchaserId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Messages",
                newName: "PostedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Messages",
                newName: "PosterId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_CreatedBy",
                table: "Messages",
                newName: "IX_Messages_PosterId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OnlineSaleId",
                table: "Order",
                newName: "IX_Order_OnlineSaleId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_TradeItemId",
                table: "OrderItem",
                newName: "IX_OrderItem_TradeItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductShipmentId",
                table: "OrderItem",
                newName: "IX_OrderItem_ProductShipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_InventoryItemId",
                table: "OrderItem",
                newName: "IX_OrderItem_InventoryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ContractId",
                table: "OrderItem",
                newName: "IX_OrderItem_ContractId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_PosterId",
                table: "Messages",
                column: "PosterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Interactions_OnlineSaleId",
                table: "Order",
                column: "OnlineSaleId",
                principalTable: "Interactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Contracts_ContractId",
                table: "OrderItem",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_InventoryItems_InventoryItemId",
                table: "OrderItem",
                column: "InventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductShipments_ProductShipmentId",
                table: "OrderItem",
                column: "ProductShipmentId",
                principalTable: "ProductShipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_TradeItems_TradeItemId",
                table: "OrderItem",
                column: "TradeItemId",
                principalTable: "TradeItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShipments_Order_OrderId",
                table: "ProductShipments",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
