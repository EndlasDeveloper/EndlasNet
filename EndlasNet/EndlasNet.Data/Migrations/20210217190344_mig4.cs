using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachiningTools_MachiningToolForJob_ToolToJobMachiningToolForJobId",
                table: "MachiningTools");

            migrationBuilder.DropTable(
                name: "MachiningToolForJob");

            migrationBuilder.DropTable(
                name: "MachiningToolForWorkOrder");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("d33ec20d-a31c-48b1-9b6a-47398f59fa69"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("5e6a83b9-82fa-40c3-8823-ce6b7910a24e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6e53952c-4e7f-4099-aaa5-2372da0babe4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8584c7e9-3805-4651-a3dd-417af1f4df7c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8d8bb03c-dd7e-47ca-850b-939fe1fbb756"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("f1fef872-65f7-4411-b427-2bc1642df7ed"));

            migrationBuilder.RenameColumn(
                name: "ToolToJobMachiningToolForJobId",
                table: "MachiningTools",
                newName: "ToolToJobMachiningToolForWorkId");

            migrationBuilder.RenameIndex(
                name: "IX_MachiningTools_ToolToJobMachiningToolForJobId",
                table: "MachiningTools",
                newName: "IX_MachiningTools_ToolToJobMachiningToolForWorkId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MachiningToolForWork",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "MachiningToolForWork",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "MachiningToolForWork",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("728915c4-0a2c-4953-b2c9-413f9b617005"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("74289544-9d79-414c-9bf3-de99e6c6ed79"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "SA", "SA" },
                    { new Guid("75ad8327-6f3c-4d8b-ae85-0d19e27e2963"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "James", "Tomich" },
                    { new Guid("0542df51-415f-40cc-a79d-4f36e8689d8b"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("1cd10d3f-17af-40c2-a3eb-c9f7fb67b7b5"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("e8f06d9c-9999-484c-b7cd-28cc37e864df"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.AddForeignKey(
                name: "FK_MachiningTools_MachiningToolForWork_ToolToJobMachiningToolForWorkId",
                table: "MachiningTools",
                column: "ToolToJobMachiningToolForWorkId",
                principalTable: "MachiningToolForWork",
                principalColumn: "MachiningToolForWorkId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachiningTools_MachiningToolForWork_ToolToJobMachiningToolForWorkId",
                table: "MachiningTools");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("728915c4-0a2c-4953-b2c9-413f9b617005"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("0542df51-415f-40cc-a79d-4f36e8689d8b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("1cd10d3f-17af-40c2-a3eb-c9f7fb67b7b5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("74289544-9d79-414c-9bf3-de99e6c6ed79"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("75ad8327-6f3c-4d8b-ae85-0d19e27e2963"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("e8f06d9c-9999-484c-b7cd-28cc37e864df"));

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MachiningToolForWork");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "MachiningToolForWork");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "MachiningToolForWork");

            migrationBuilder.RenameColumn(
                name: "ToolToJobMachiningToolForWorkId",
                table: "MachiningTools",
                newName: "ToolToJobMachiningToolForJobId");

            migrationBuilder.RenameIndex(
                name: "IX_MachiningTools_ToolToJobMachiningToolForWorkId",
                table: "MachiningTools",
                newName: "IX_MachiningTools_ToolToJobMachiningToolForJobId");

            migrationBuilder.CreateTable(
                name: "MachiningToolForJob",
                columns: table => new
                {
                    MachiningToolForJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateUsed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachiningToolForJob", x => x.MachiningToolForJobId);
                    table.ForeignKey(
                        name: "FK_MachiningToolForJob_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachiningToolForJob_Work_JobId",
                        column: x => x.JobId,
                        principalTable: "Work",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachiningToolForWorkOrder",
                columns: table => new
                {
                    MachiningToolForJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUsed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachiningToolForWorkOrder", x => x.MachiningToolForJobId);
                    table.ForeignKey(
                        name: "FK_MachiningToolForWorkOrder_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachiningToolForWorkOrder_Work_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "Work",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("d33ec20d-a31c-48b1-9b6a-47398f59fa69"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("8584c7e9-3805-4651-a3dd-417af1f4df7c"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "SA", "SA" },
                    { new Guid("8d8bb03c-dd7e-47ca-850b-939fe1fbb756"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "James", "Tomich" },
                    { new Guid("5e6a83b9-82fa-40c3-8823-ce6b7910a24e"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("6e53952c-4e7f-4099-aaa5-2372da0babe4"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("f1fef872-65f7-4411-b427-2bc1642df7ed"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolForJob_JobId",
                table: "MachiningToolForJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolForJob_UserId",
                table: "MachiningToolForJob",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolForWorkOrder_UserId",
                table: "MachiningToolForWorkOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolForWorkOrder_WorkOrderId",
                table: "MachiningToolForWorkOrder",
                column: "WorkOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachiningTools_MachiningToolForJob_ToolToJobMachiningToolForJobId",
                table: "MachiningTools",
                column: "ToolToJobMachiningToolForJobId",
                principalTable: "MachiningToolForJob",
                principalColumn: "MachiningToolForJobId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
