using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("049a40f3-8405-441f-b342-2b21fa303ed8"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("752532a2-9807-46ba-b5cf-8dbbf9ba642d"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "SA", "SA" },
                    { new Guid("068f3a36-f840-4fa3-a65f-b2478764b7e9"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "James", "Tomich" },
                    { new Guid("997159de-23b6-4f44-837f-ea41ec3472df"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("f22ab958-a884-49fa-81cc-715891b0db08"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("637f9b56-ea49-4748-9f34-056c08cd7819"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("049a40f3-8405-441f-b342-2b21fa303ed8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("068f3a36-f840-4fa3-a65f-b2478764b7e9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("752532a2-9807-46ba-b5cf-8dbbf9ba642d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("997159de-23b6-4f44-837f-ea41ec3472df"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("f22ab958-a884-49fa-81cc-715891b0db08"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("637f9b56-ea49-4748-9f34-056c08cd7819"));

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
    }
}
