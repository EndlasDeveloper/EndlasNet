using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartsForWork_StaticPartInfo_StaticPartInfoId",
                table: "PartsForWork");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_StaticPartInfo_StaticPartInfoId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_PartsForWork_StaticPartInfoId",
                table: "PartsForWork");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("33d73f3c-987e-4c71-9360-7283e65022d3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("1117954b-a166-40e7-aff6-7cf7480e9986"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("89e5ab8b-e824-43f5-9a56-ae0fa8388c00"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8af38518-f71e-4498-b53e-fb1538f67baf"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d2742452-55d0-426d-909c-6ccc4399da45"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("adab2384-bfb9-494c-a5dc-90ba00603df2"));

            migrationBuilder.DropColumn(
                name: "StaticPartInfoId",
                table: "PartsForWork");

            migrationBuilder.AlterColumn<Guid>(
                name: "StaticPartInfoId",
                table: "WorkItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("3da8b720-4c4a-424e-ab5a-1703318c4184"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("d92f3281-558c-4502-b4ba-da3ec6c2e066"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("a527c4b9-b52a-4c4e-833b-10bedc791266"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("04acf981-1a1e-4c9a-b420-c252cb033bb1"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("8b60272b-a595-45a2-aac9-c10183069908"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("a707a393-8397-4663-b2a2-f085da35c5d3"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_StaticPartInfo_StaticPartInfoId",
                table: "WorkItems",
                column: "StaticPartInfoId",
                principalTable: "StaticPartInfo",
                principalColumn: "StaticPartInfoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_StaticPartInfo_StaticPartInfoId",
                table: "WorkItems");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("3da8b720-4c4a-424e-ab5a-1703318c4184"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("04acf981-1a1e-4c9a-b420-c252cb033bb1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8b60272b-a595-45a2-aac9-c10183069908"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a527c4b9-b52a-4c4e-833b-10bedc791266"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d92f3281-558c-4502-b4ba-da3ec6c2e066"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("a707a393-8397-4663-b2a2-f085da35c5d3"));

            migrationBuilder.AlterColumn<Guid>(
                name: "StaticPartInfoId",
                table: "WorkItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StaticPartInfoId",
                table: "PartsForWork",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("33d73f3c-987e-4c71-9360-7283e65022d3"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("1117954b-a166-40e7-aff6-7cf7480e9986"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("89e5ab8b-e824-43f5-9a56-ae0fa8388c00"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("d2742452-55d0-426d-909c-6ccc4399da45"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("8af38518-f71e-4498-b53e-fb1538f67baf"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("adab2384-bfb9-494c-a5dc-90ba00603df2"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.CreateIndex(
                name: "IX_PartsForWork_StaticPartInfoId",
                table: "PartsForWork",
                column: "StaticPartInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartsForWork_StaticPartInfo_StaticPartInfoId",
                table: "PartsForWork",
                column: "StaticPartInfoId",
                principalTable: "StaticPartInfo",
                principalColumn: "StaticPartInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_StaticPartInfo_StaticPartInfoId",
                table: "WorkItems",
                column: "StaticPartInfoId",
                principalTable: "StaticPartInfo",
                principalColumn: "StaticPartInfoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
