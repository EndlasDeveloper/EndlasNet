using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("4716b232-9c2e-4eb8-a0a1-0940835d9bd6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("371b03d0-1592-4b90-8e26-48e0b75dd182"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("38b2dea1-6f94-4963-9279-0061acb07d22"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("5bfa27b5-4e35-4685-bc18-d9a4033196c6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6f456d21-e268-411a-8709-837363b03001"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("637d33c3-ac46-46eb-8259-a883b759e11d"));

            migrationBuilder.DropColumn(
                name: "NumBottles",
                table: "LineItems");

            migrationBuilder.AddColumn<int>(
                name: "NumLineItems",
                table: "PowderOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("b4d02721-6604-4a11-9376-c22edca90a9e"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("1be4bb5f-8471-4b05-a432-c5fdbc464dcb"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "SA", "SA" },
                    { new Guid("0d2a5808-aa2f-4454-9d94-b5246e098745"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "James", "Tomich" },
                    { new Guid("251ec222-e328-448c-a9dc-5c1be737346b"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("5f784a81-42f3-4639-a607-2396415f5745"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("2ef4ea6e-077c-46ef-ad74-465459f9b775"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("b4d02721-6604-4a11-9376-c22edca90a9e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("0d2a5808-aa2f-4454-9d94-b5246e098745"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("1be4bb5f-8471-4b05-a432-c5fdbc464dcb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("251ec222-e328-448c-a9dc-5c1be737346b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("5f784a81-42f3-4639-a607-2396415f5745"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("2ef4ea6e-077c-46ef-ad74-465459f9b775"));

            migrationBuilder.DropColumn(
                name: "NumLineItems",
                table: "PowderOrders");

            migrationBuilder.AddColumn<int>(
                name: "NumBottles",
                table: "LineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("4716b232-9c2e-4eb8-a0a1-0940835d9bd6"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("5bfa27b5-4e35-4685-bc18-d9a4033196c6"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "SA", "SA" },
                    { new Guid("6f456d21-e268-411a-8709-837363b03001"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "James", "Tomich" },
                    { new Guid("371b03d0-1592-4b90-8e26-48e0b75dd182"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("38b2dea1-6f94-4963-9279-0061acb07d22"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("637d33c3-ac46-46eb-8259-a883b759e11d"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });
        }
    }
}
