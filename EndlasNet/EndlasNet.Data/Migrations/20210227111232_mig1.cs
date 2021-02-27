using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItem_PowderOrder_PowderOrderId",
                table: "LineItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PowderOrder_Vendors_VendorId",
                table: "PowderOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Powders_LineItem_LineItemId",
                table: "Powders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowderOrder",
                table: "PowderOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LineItem",
                table: "LineItem");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("4746f936-3d84-47e6-b0c9-b0f3c9c0ed3c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("736dc604-4f18-44b4-9621-3d470d78525e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("92f6d31c-9b53-4043-b8b0-22882a81ef83"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9fd45980-07a0-48de-aa2e-3890613080b3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ff49066b-9a0e-438d-b191-eedaf35ee368"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("fc890752-1668-45fe-90b2-894251eecc66"));

            migrationBuilder.RenameTable(
                name: "PowderOrder",
                newName: "PowderOrders");

            migrationBuilder.RenameTable(
                name: "LineItem",
                newName: "LineItems");

            migrationBuilder.RenameIndex(
                name: "IX_PowderOrder_VendorId",
                table: "PowderOrders",
                newName: "IX_PowderOrders_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_LineItem_PowderOrderId",
                table: "LineItems",
                newName: "IX_LineItems_PowderOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowderOrders",
                table: "PowderOrders",
                column: "PowderOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineItems",
                table: "LineItems",
                column: "LineItemId");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("6ffb5e5d-c553-4b52-8c96-14f601ee2d5d"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("ec371f0c-c115-4109-9ed6-638256387aa8"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "SA", "SA" },
                    { new Guid("7a0cef6e-a587-418b-9b84-7cc7d976d6b7"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "James", "Tomich" },
                    { new Guid("963c5e6f-6b0d-4317-b3f7-76bbca830865"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("07356a5e-58ed-44d6-9f92-70e3609bb726"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("5d130d77-a055-4eba-843f-4b1eccd1a57c"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_PowderOrders_PowderOrderId",
                table: "LineItems",
                column: "PowderOrderId",
                principalTable: "PowderOrders",
                principalColumn: "PowderOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowderOrders_Vendors_VendorId",
                table: "PowderOrders",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Powders_LineItems_LineItemId",
                table: "Powders",
                column: "LineItemId",
                principalTable: "LineItems",
                principalColumn: "LineItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_PowderOrders_PowderOrderId",
                table: "LineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PowderOrders_Vendors_VendorId",
                table: "PowderOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Powders_LineItems_LineItemId",
                table: "Powders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowderOrders",
                table: "PowderOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LineItems",
                table: "LineItems");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("6ffb5e5d-c553-4b52-8c96-14f601ee2d5d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("07356a5e-58ed-44d6-9f92-70e3609bb726"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("7a0cef6e-a587-418b-9b84-7cc7d976d6b7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("963c5e6f-6b0d-4317-b3f7-76bbca830865"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ec371f0c-c115-4109-9ed6-638256387aa8"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("5d130d77-a055-4eba-843f-4b1eccd1a57c"));

            migrationBuilder.RenameTable(
                name: "PowderOrders",
                newName: "PowderOrder");

            migrationBuilder.RenameTable(
                name: "LineItems",
                newName: "LineItem");

            migrationBuilder.RenameIndex(
                name: "IX_PowderOrders_VendorId",
                table: "PowderOrder",
                newName: "IX_PowderOrder_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_LineItems_PowderOrderId",
                table: "LineItem",
                newName: "IX_LineItem_PowderOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowderOrder",
                table: "PowderOrder",
                column: "PowderOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineItem",
                table: "LineItem",
                column: "LineItemId");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("4746f936-3d84-47e6-b0c9-b0f3c9c0ed3c"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("9fd45980-07a0-48de-aa2e-3890613080b3"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "SA", "SA" },
                    { new Guid("736dc604-4f18-44b4-9621-3d470d78525e"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "James", "Tomich" },
                    { new Guid("92f6d31c-9b53-4043-b8b0-22882a81ef83"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("ff49066b-9a0e-438d-b191-eedaf35ee368"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("fc890752-1668-45fe-90b2-894251eecc66"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.AddForeignKey(
                name: "FK_LineItem_PowderOrder_PowderOrderId",
                table: "LineItem",
                column: "PowderOrderId",
                principalTable: "PowderOrder",
                principalColumn: "PowderOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowderOrder_Vendors_VendorId",
                table: "PowderOrder",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Powders_LineItem_LineItemId",
                table: "Powders",
                column: "LineItemId",
                principalTable: "LineItem",
                principalColumn: "LineItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
