using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachiningToolsForWork_Work_WorkId",
                table: "MachiningToolsForWork");

            migrationBuilder.DropForeignKey(
                name: "FK_PartsForWork_Users_UserId",
                table: "PartsForWork");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Users_UserId",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Work_Users_UserId",
                table: "Work");

            migrationBuilder.DropIndex(
                name: "IX_Work_UserId",
                table: "Work");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_PartsForWork_UserId",
                table: "PartsForWork");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("af5c56e2-4295-4f1f-b7c2-2e2175756dc8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("48cd3b4d-6a47-451c-8478-f24a70065279"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("72b0c8a4-437d-4642-b3fd-a0ade598d375"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d4059044-30e6-4569-b41a-d9ffa2350bba"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("e188e8de-e4bd-4c46-a176-49ead0025648"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("260e106b-bdc9-4af2-92e6-f4edf8adda46"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Work");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PartsForWork");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "MachiningToolsForWork");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkItemId",
                table: "MachiningToolsForWork",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("52324023-aaaf-4b20-8142-bbd8f15dcfda"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("b605cf09-6cd7-42bc-a605-35c30aa906f7"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("9d0a9a71-4f3c-4143-8af8-b7d6f23d41c5"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("1c4c0d23-1428-487a-9b86-c889496bc032"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("ddbd0c11-1543-4ae5-9bd2-1b7d44d51231"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("fb962c97-4394-4fec-9b34-2a58dd4d9186"), "Dummy Point of Contact", "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolsForWork_WorkItemId",
                table: "MachiningToolsForWork",
                column: "WorkItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachiningToolsForWork_Work_WorkId",
                table: "MachiningToolsForWork",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MachiningToolsForWork_WorkItems_WorkItemId",
                table: "MachiningToolsForWork",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "WorkItemId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachiningToolsForWork_Work_WorkId",
                table: "MachiningToolsForWork");

            migrationBuilder.DropForeignKey(
                name: "FK_MachiningToolsForWork_WorkItems_WorkItemId",
                table: "MachiningToolsForWork");

            migrationBuilder.DropIndex(
                name: "IX_MachiningToolsForWork_WorkItemId",
                table: "MachiningToolsForWork");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("52324023-aaaf-4b20-8142-bbd8f15dcfda"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("1c4c0d23-1428-487a-9b86-c889496bc032"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9d0a9a71-4f3c-4143-8af8-b7d6f23d41c5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b605cf09-6cd7-42bc-a605-35c30aa906f7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ddbd0c11-1543-4ae5-9bd2-1b7d44d51231"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("fb962c97-4394-4fec-9b34-2a58dd4d9186"));

            migrationBuilder.DropColumn(
                name: "WorkItemId",
                table: "MachiningToolsForWork");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Work",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Vendors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PartsForWork",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "MachiningToolsForWork",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("af5c56e2-4295-4f1f-b7c2-2e2175756dc8"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("72b0c8a4-437d-4642-b3fd-a0ade598d375"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("e188e8de-e4bd-4c46-a176-49ead0025648"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("d4059044-30e6-4569-b41a-d9ffa2350bba"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("48cd3b4d-6a47-451c-8478-f24a70065279"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("260e106b-bdc9-4af2-92e6-f4edf8adda46"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.CreateIndex(
                name: "IX_Work_UserId",
                table: "Work",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsForWork_UserId",
                table: "PartsForWork",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachiningToolsForWork_Work_WorkId",
                table: "MachiningToolsForWork",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PartsForWork_Users_UserId",
                table: "PartsForWork",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Users_UserId",
                table: "Vendors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Work_Users_UserId",
                table: "Work",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
