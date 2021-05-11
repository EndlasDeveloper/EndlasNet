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
                keyValue: new Guid("7c199b22-eb5c-4981-a9b7-95167ddcbddc"));


            migrationBuilder.AddColumn<int>(
                name: "AnotherFakeProperty",
                table: "PowderForParts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("1b3ca32a-8689-4758-8aca-1b62e0dec421"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

       
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("1b3ca32a-8689-4758-8aca-1b62e0dec421"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("103985a5-1ad1-469d-90fe-9a04432d32e4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c4890d86-9248-447c-917c-bf4ab96db537"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ccbc6e88-899b-4bf0-a385-b5cc4b40571c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("e2a48e36-7a9c-4b88-91e8-543426130aac"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("f39d560c-61c6-480c-be2c-9374bd3c84e9"));

            migrationBuilder.DropColumn(
                name: "AnotherFakeProperty",
                table: "PowderForParts");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("7c199b22-eb5c-4981-a9b7-95167ddcbddc"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("f81dba9a-b87f-4810-981f-49e63c30028e"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("5b6474f5-45a6-4d46-bfee-498341b98923"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("3676d32c-0029-48ea-987b-ed019e3b750a"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("e87206a1-554f-4fcc-a6b0-0588da91894f"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("47de9748-35eb-4378-b3c7-2b561f76ef96"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });
        }
    }
}
