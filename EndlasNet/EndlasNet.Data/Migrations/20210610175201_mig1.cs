using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartsForWork_WorkItem_WorkItemId",
                table: "PartsForWork");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItem_Work_WorkId",
                table: "WorkItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkItem",
                table: "WorkItem");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("523003d8-ac1a-41d4-870d-8b5f3c218023"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("1e10ef4b-fbe0-4287-ae35-4a970c40600b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("65aa8e46-734a-472a-b6e9-ca377ec53c87"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("83eaf924-42fb-4a59-80d8-74649a8b8ce1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9d77d66f-e6e4-4758-b308-e0dbaa413fbb"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("1366fada-7e27-4dd0-8a9e-4c999d4b2232"));

            migrationBuilder.RenameTable(
                name: "WorkItem",
                newName: "WorkItems");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItem_WorkId",
                table: "WorkItems",
                newName: "IX_WorkItems_WorkId");

            migrationBuilder.AddColumn<bool>(
                name: "IsInitialized",
                table: "WorkItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkItems",
                table: "WorkItems",
                column: "WorkItemId");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("3ce2fea1-d038-44ba-adc5-4edea385bc2d"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("9d014bee-05d8-4f31-87d7-9ec32e5e2b2c"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("98853bd4-e445-48ff-a422-67c7db1d8921"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("d7ad5b1f-93cd-443c-ab6e-0477c0d0604e"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("e977119e-9a90-4db8-9eb0-854cba3d4288"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("88ad9135-5482-4127-ac46-4b08dfc209f3"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.AddForeignKey(
                name: "FK_PartsForWork_WorkItems_WorkItemId",
                table: "PartsForWork",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "WorkItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Work_WorkId",
                table: "WorkItems",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartsForWork_WorkItems_WorkItemId",
                table: "PartsForWork");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Work_WorkId",
                table: "WorkItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkItems",
                table: "WorkItems");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("3ce2fea1-d038-44ba-adc5-4edea385bc2d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("98853bd4-e445-48ff-a422-67c7db1d8921"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9d014bee-05d8-4f31-87d7-9ec32e5e2b2c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d7ad5b1f-93cd-443c-ab6e-0477c0d0604e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("e977119e-9a90-4db8-9eb0-854cba3d4288"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("88ad9135-5482-4127-ac46-4b08dfc209f3"));

            migrationBuilder.DropColumn(
                name: "IsInitialized",
                table: "WorkItems");

            migrationBuilder.RenameTable(
                name: "WorkItems",
                newName: "WorkItem");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItems_WorkId",
                table: "WorkItem",
                newName: "IX_WorkItem_WorkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkItem",
                table: "WorkItem",
                column: "WorkItemId");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("523003d8-ac1a-41d4-870d-8b5f3c218023"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("1e10ef4b-fbe0-4287-ae35-4a970c40600b"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("83eaf924-42fb-4a59-80d8-74649a8b8ce1"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("65aa8e46-734a-472a-b6e9-ca377ec53c87"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("9d77d66f-e6e4-4758-b308-e0dbaa413fbb"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("1366fada-7e27-4dd0-8a9e-4c999d4b2232"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.AddForeignKey(
                name: "FK_PartsForWork_WorkItem_WorkItemId",
                table: "PartsForWork",
                column: "WorkItemId",
                principalTable: "WorkItem",
                principalColumn: "WorkItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItem_Work_WorkId",
                table: "WorkItem",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
