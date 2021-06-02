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
                keyValue: new Guid("4fff5520-3aa9-4dd0-87fd-91c4791705dd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("1485b416-ca69-49af-8070-4258cd8b2a24"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("56608b30-6600-4aa3-89ad-14cbcc85b257"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bf64312f-646c-477e-9c2d-c0cf48e1818a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("f61559ad-5c97-4687-8f78-9b051b4b02df"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("390f8635-bdcb-442f-8c30-b91c9ac8457e"));

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "PartsForWork");

            migrationBuilder.AddColumn<byte[]>(
                name: "CladdingImageBytes",
                table: "PartsForWork",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FinishedImageBytes",
                table: "PartsForWork",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "MachiningImageBytes",
                table: "PartsForWork",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "UsedImageBytes",
                table: "PartsForWork",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("b9543749-e50e-42aa-acc8-88922bf3a318"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("f9982320-a286-4539-818c-b26c8cb09773"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("c52d8862-0fef-4d20-80cb-fa6b31d8c809"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("78d88714-9cc8-45b3-969b-31fb1c567f52"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("d02d0a16-d27c-4438-bbea-d9f912961107"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("c8c07b94-9ca2-45d2-a402-d689ae6027a5"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("b9543749-e50e-42aa-acc8-88922bf3a318"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("78d88714-9cc8-45b3-969b-31fb1c567f52"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c52d8862-0fef-4d20-80cb-fa6b31d8c809"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d02d0a16-d27c-4438-bbea-d9f912961107"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("f9982320-a286-4539-818c-b26c8cb09773"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("c8c07b94-9ca2-45d2-a402-d689ae6027a5"));

            migrationBuilder.DropColumn(
                name: "CladdingImageBytes",
                table: "PartsForWork");

            migrationBuilder.DropColumn(
                name: "FinishedImageBytes",
                table: "PartsForWork");

            migrationBuilder.DropColumn(
                name: "MachiningImageBytes",
                table: "PartsForWork");

            migrationBuilder.DropColumn(
                name: "UsedImageBytes",
                table: "PartsForWork");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "PartsForWork",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("4fff5520-3aa9-4dd0-87fd-91c4791705dd"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("bf64312f-646c-477e-9c2d-c0cf48e1818a"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("56608b30-6600-4aa3-89ad-14cbcc85b257"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("f61559ad-5c97-4687-8f78-9b051b4b02df"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("1485b416-ca69-49af-8070-4258cd8b2a24"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("390f8635-bdcb-442f-8c30-b91c9ac8457e"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });
        }
    }
}
