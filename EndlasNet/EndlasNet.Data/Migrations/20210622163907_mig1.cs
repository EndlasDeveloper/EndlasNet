using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig1 : Migration
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
                name: "FK_WorkItems_StaticPartInfo_StaticPartInfoId",
                table: "WorkItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Work_WorkId",
                table: "WorkItems");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("6cc6cbc4-19fa-4569-ab0e-fcf5ac9804d0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("08b769b2-80c5-4aec-8456-314b070fcd9c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a27fdc86-6924-4485-b965-b4e1e8a9d3dd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("e0b69f7b-f9c7-4227-924e-411096e07a7f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ecad6f4d-0c46-4f70-8378-d3bd83473e92"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("82c9acfe-264e-4509-ad9d-7f22b2b66e53"));

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
                name: "FK_WorkItems_StaticPartInfo_StaticPartInfoId",
                table: "WorkItems",
                column: "StaticPartInfoId",
                principalTable: "StaticPartInfo",
                principalColumn: "StaticPartInfoId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Work_WorkId",
                table: "WorkItems",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.SetNull);
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
                name: "FK_WorkItems_StaticPartInfo_StaticPartInfoId",
                table: "WorkItems");

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
                values: new object[] { new Guid("6cc6cbc4-19fa-4569-ab0e-fcf5ac9804d0"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("a27fdc86-6924-4485-b965-b4e1e8a9d3dd"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "sa@endlas.com", "SA", "SA" },
                    { new Guid("ecad6f4d-0c46-4f70-8378-d3bd83473e92"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "Jimmy", "Tomich" },
                    { new Guid("e0b69f7b-f9c7-4227-924e-411096e07a7f"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("08b769b2-80c5-4aec-8456-314b070fcd9c"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("82c9acfe-264e-4509-ad9d-7f22b2b66e53"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.AddForeignKey(
                name: "FK_PartsForWork_WorkItems_WorkItemId",
                table: "PartsForWork",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "WorkItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PowderForParts_PartsForWork_PartForWorkId",
                table: "PowderForParts",
                column: "PartForWorkId",
                principalTable: "PartsForWork",
                principalColumn: "PartForWorkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_StaticPartInfo_StaticPartInfoId",
                table: "WorkItems",
                column: "StaticPartInfoId",
                principalTable: "StaticPartInfo",
                principalColumn: "StaticPartInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Work_WorkId",
                table: "WorkItems",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
