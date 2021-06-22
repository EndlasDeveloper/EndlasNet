using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartsForWork_WorkItems_WorkItemId",
                table: "PartsForWork");

            migrationBuilder.DropForeignKey(
                name: "FK_PowderForParts_PartsForWork_PartForWorkId",
                table: "PowderForParts");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Work_WorkId",
                table: "WorkItems");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("718169a6-c4f2-4f74-89c3-de61452f86d4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6ae50f7e-4822-4c1a-9fb2-ef5a5809464d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9d38bb83-9393-4df1-b4cf-d1e2fce1d7fa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c0922b46-ac57-408a-84d1-802c2c7dba96"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c40d2230-93dc-478c-a30c-a534b435b1f9"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("644c3bfb-13fe-406e-94a5-5dfdf5125aed"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("4ee4bb1a-2f07-4dbc-beba-8e606d0f3319"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("8dcdc340-211c-43c0-bd49-19efdd0c7311"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("6511e25e-9f5f-4668-88d9-c374530c286d"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("3e7471d9-4204-436b-b901-9363e92622c1"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("3ecc6a51-9d7a-4a3f-9563-de7e3914cc6c"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("b57c58d0-e29b-466f-a0a7-3862e2f27c38"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.AddForeignKey(
                name: "FK_PartsForWork_WorkItems_WorkItemId",
                table: "PartsForWork",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "WorkItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowderForParts_PartsForWork_PartForWorkId",
                table: "PowderForParts",
                column: "PartForWorkId",
                principalTable: "PartsForWork",
                principalColumn: "PartForWorkId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_PowderForParts_PartsForWork_PartForWorkId",
                table: "PowderForParts");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Work_WorkId",
                table: "WorkItems");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("4ee4bb1a-2f07-4dbc-beba-8e606d0f3319"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("3e7471d9-4204-436b-b901-9363e92622c1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("3ecc6a51-9d7a-4a3f-9563-de7e3914cc6c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6511e25e-9f5f-4668-88d9-c374530c286d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8dcdc340-211c-43c0-bd49-19efdd0c7311"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("b57c58d0-e29b-466f-a0a7-3862e2f27c38"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("718169a6-c4f2-4f74-89c3-de61452f86d4"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("9d38bb83-9393-4df1-b4cf-d1e2fce1d7fa"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("6ae50f7e-4822-4c1a-9fb2-ef5a5809464d"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("c0922b46-ac57-408a-84d1-802c2c7dba96"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("c40d2230-93dc-478c-a30c-a534b435b1f9"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("644c3bfb-13fe-406e-94a5-5dfdf5125aed"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.AddForeignKey(
                name: "FK_PartsForWork_WorkItems_WorkItemId",
                table: "PartsForWork",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "WorkItemId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PowderForParts_PartsForWork_PartForWorkId",
                table: "PowderForParts",
                column: "PartForWorkId",
                principalTable: "PartsForWork",
                principalColumn: "PartForWorkId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Work_WorkId",
                table: "WorkItems",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
