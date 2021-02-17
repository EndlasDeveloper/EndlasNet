using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartsForJobs");

            migrationBuilder.DropTable(
                name: "PartsForWorkOrders");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("132de74b-df8a-4a10-bf6a-d11062159514"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("644a6b3b-523e-4df3-aa27-185f076b2e4a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b2a9d57c-cf45-482e-a014-e5e6045662c0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c410a61b-4b05-409d-acbc-613c4ff08405"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d33fbb50-9624-4aca-8580-959c3904e3b3"));

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: new Guid("bfad60af-9777-4df4-9113-2faa0a0c7388"));

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MachiningToolForJob");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "MachiningToolForJob");

            migrationBuilder.CreateTable(
                name: "PartsForWork",
                columns: table => new
                {
                    PartForWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartsForWork", x => x.PartForWorkId);
                    table.ForeignKey(
                        name: "FK_PartsForWork_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartsForWork_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartsForWork_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_PartsForWork_PartId",
                table: "PartsForWork",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsForWork_UserId",
                table: "PartsForWork",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsForWork_WorkId",
                table: "PartsForWork",
                column: "WorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartsForWork");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MachiningToolForJob",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "MachiningToolForJob",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "PartsForJobs",
                columns: table => new
                {
                    PartForWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartsForJobs", x => x.PartForWorkId);
                    table.ForeignKey(
                        name: "FK_PartsForJobs_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartsForJobs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartsForJobs_Work_JobId",
                        column: x => x.JobId,
                        principalTable: "Work",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartsForWorkOrders",
                columns: table => new
                {
                    PartForWorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartsForWorkOrders", x => x.PartForWorkOrderId);
                    table.ForeignKey(
                        name: "FK_PartsForWorkOrders_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartsForWorkOrders_Work_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "Work",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerAddress", "CustomerName", "CustomerPhone", "PointOfContact" },
                values: new object[] { new Guid("132de74b-df8a-4a10-bf6a-d11062159514"), "Dummy Customer Address", "Dummy Customer Name", "0987654321", "Dummy Point of Contact" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("b2a9d57c-cf45-482e-a014-e5e6045662c0"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "SA", "SA" },
                    { new Guid("c410a61b-4b05-409d-acbc-613c4ff08405"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "james.tomich@endlas.com", "James", "Tomich" },
                    { new Guid("644a6b3b-523e-4df3-aa27-185f076b2e4a"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "josh.hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("d33fbb50-9624-4aca-8580-959c3904e3b3"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "blt@endlas.com", "Brett", "Trotter" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "PointOfContact", "UserId", "VendorAddress", "VendorName", "VendorPhone" },
                values: new object[] { new Guid("bfad60af-9777-4df4-9113-2faa0a0c7388"), "Dummy Point of Contact", null, "Dummy Vendor Address", "Dummy Vendor Name", "1234567890" });

            migrationBuilder.CreateIndex(
                name: "IX_PartsForJobs_JobId",
                table: "PartsForJobs",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsForJobs_PartId",
                table: "PartsForJobs",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsForJobs_UserId",
                table: "PartsForJobs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsForWorkOrders_PartId",
                table: "PartsForWorkOrders",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_PartsForWorkOrders_WorkOrderId",
                table: "PartsForWorkOrders",
                column: "WorkOrderId");
        }
    }
}
