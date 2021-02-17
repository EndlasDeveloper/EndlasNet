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
                keyValue: new Guid("adf421ff-03a7-453d-867c-e744d918015f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("27aacee3-0cb3-4975-873e-95b86129046a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("2c497d9f-b8c7-4850-afd5-5bc8f41f7c08"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a1c67cc3-41ae-491b-a683-aa18b372444b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("f911e8dc-1fe2-4007-9b06-429ab49f6820"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("a30b7fb2-c588-47ed-b21a-c5cf06b19b94"));

            migrationBuilder.AddColumn<Guid>(
                name: "StaticPartInfoId",
                table: "Parts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StaticPartInfo",
                columns: table => new
                {
                    StaticPartInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DrawingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConditionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessingNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrawingImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPartInfo", x => x.StaticPartInfoId);
                    table.ForeignKey(
                        name: "FK_StaticPartInfo_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaticPartInfo_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("8143e38d-6385-4e68-9c3b-050cc0250cc2"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("3795e60e-9ea1-4d33-a0cf-4f192f012a61"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "SA", "SA" },
                    { new Guid("8346e683-eb93-4594-a6fb-a6a4c32a84c4"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "James", "Tomich" },
                    { new Guid("ac960e0c-dd3a-448c-b913-2dd343daecc4"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("030f72bf-2bd0-449d-b303-5c7238c37327"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("b23ff53a-4799-4b4c-a498-1d3ba6d4e6ac"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_StaticPartInfoId",
                table: "Parts",
                column: "StaticPartInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPartInfo_CustomerId",
                table: "StaticPartInfo",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPartInfo_UserId",
                table: "StaticPartInfo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_StaticPartInfo_StaticPartInfoId",
                table: "Parts",
                column: "StaticPartInfoId",
                principalTable: "StaticPartInfo",
                principalColumn: "StaticPartInfoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_StaticPartInfo_StaticPartInfoId",
                table: "Parts");

            migrationBuilder.DropTable(
                name: "StaticPartInfo");

            migrationBuilder.DropIndex(
                name: "IX_Parts_StaticPartInfoId",
                table: "Parts");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("8143e38d-6385-4e68-9c3b-050cc0250cc2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("030f72bf-2bd0-449d-b303-5c7238c37327"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("3795e60e-9ea1-4d33-a0cf-4f192f012a61"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8346e683-eb93-4594-a6fb-a6a4c32a84c4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ac960e0c-dd3a-448c-b913-2dd343daecc4"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("b23ff53a-4799-4b4c-a498-1d3ba6d4e6ac"));

            migrationBuilder.DropColumn(
                name: "StaticPartInfoId",
                table: "Parts");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("adf421ff-03a7-453d-867c-e744d918015f"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("2c497d9f-b8c7-4850-afd5-5bc8f41f7c08"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "SA", "SA" },
                    { new Guid("a1c67cc3-41ae-491b-a683-aa18b372444b"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "James", "Tomich" },
                    { new Guid("f911e8dc-1fe2-4007-9b06-429ab49f6820"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("27aacee3-0cb3-4975-873e-95b86129046a"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("a30b7fb2-c588-47ed-b21a-c5cf06b19b94"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });
        }
    }
}
