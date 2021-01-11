using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_products_ProductId",
                table: "inventories");

            migrationBuilder.DropIndex(
                name: "IX_inventories_ProductId",
                table: "inventories");

            migrationBuilder.DropIndex(
                name: "IX_inventories_StoreLocationId",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "ProductQuantity",
                table: "inventories");

            migrationBuilder.AddColumn<Guid>(
                name: "InventoryId",
                table: "products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_InventoryId",
                table: "products",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_StoreLocationId",
                table: "inventories",
                column: "StoreLocationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_products_inventories_InventoryId",
                table: "products",
                column: "InventoryId",
                principalTable: "inventories",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_inventories_InventoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_InventoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_inventories_StoreLocationId",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "ProductQuantity",
                table: "inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_inventories_ProductId",
                table: "inventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_StoreLocationId",
                table: "inventories",
                column: "StoreLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_products_ProductId",
                table: "inventories",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
