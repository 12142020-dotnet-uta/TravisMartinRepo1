using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_products_ProductId",
                table: "inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_storeLocations_StoreLocationId",
                table: "inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory",
                table: "inventory");

            migrationBuilder.RenameTable(
                name: "inventory",
                newName: "inventories");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_StoreLocationId",
                table: "inventories",
                newName: "IX_inventories_StoreLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_ProductId",
                table: "inventories",
                newName: "IX_inventories_ProductId");

            migrationBuilder.AddColumn<byte[]>(
                name: "ByteArrayImage",
                table: "products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventories",
                table: "inventories",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_products_ProductId",
                table: "inventories",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_storeLocations_StoreLocationId",
                table: "inventories",
                column: "StoreLocationId",
                principalTable: "storeLocations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_products_ProductId",
                table: "inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_inventories_storeLocations_StoreLocationId",
                table: "inventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventories",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "ByteArrayImage",
                table: "products");

            migrationBuilder.RenameTable(
                name: "inventories",
                newName: "inventory");

            migrationBuilder.RenameIndex(
                name: "IX_inventories_StoreLocationId",
                table: "inventory",
                newName: "IX_inventory_StoreLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_inventories_ProductId",
                table: "inventory",
                newName: "IX_inventory_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory",
                table: "inventory",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_products_ProductId",
                table: "inventory",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_storeLocations_StoreLocationId",
                table: "inventory",
                column: "StoreLocationId",
                principalTable: "storeLocations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
