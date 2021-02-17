using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointOfContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalSnapshots",
                columns: table => new
                {
                    EnvSnapshotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTimeCollected = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSnapshots", x => x.EnvSnapshotId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndlasEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthString = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointOfContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_Vendors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Work",
                columns: table => new
                {
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndlasNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.WorkId);
                    table.ForeignKey(
                        name: "FK_Work_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Work_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Powders",
                columns: table => new
                {
                    PowderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PowderName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    VendorDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PoNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BottleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticleSize = table.Column<float>(type: "real", nullable: false),
                    InitWeight = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    CostPerUnitWeight = table.Column<float>(type: "real", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powders", x => x.PowderId);
                    table.ForeignKey(
                        name: "FK_Powders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Powders_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachiningToolForJob",
                columns: table => new
                {
                    MachiningToolForJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateUsed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "MachiningToolForWork",
                columns: table => new
                {
                    MachiningToolForWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateUsed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachiningToolForWork", x => x.MachiningToolForWorkId);
                    table.ForeignKey(
                        name: "FK_MachiningToolForWork_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachiningToolForWork_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachiningToolForWorkOrder",
                columns: table => new
                {
                    MachiningToolForJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateUsed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitWeight = table.Column<float>(type: "real", nullable: false),
                    CladdedWeight = table.Column<float>(type: "real", nullable: false),
                    FinishedWeight = table.Column<float>(type: "real", nullable: false),
                    ProcessingNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_Parts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parts_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachiningTools",
                columns: table => new
                {
                    MachiningToolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToolType = table.Column<int>(type: "int", nullable: false),
                    ToolDiameter = table.Column<float>(type: "real", nullable: false),
                    VendorDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolCount = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNum = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PurchaseOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseOrderPrice = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToolToJobMachiningToolForJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachiningTools", x => x.MachiningToolId);
                    table.ForeignKey(
                        name: "FK_MachiningTools_MachiningToolForJob_ToolToJobMachiningToolForJobId",
                        column: x => x.ToolToJobMachiningToolForJobId,
                        principalTable: "MachiningToolForJob",
                        principalColumn: "MachiningToolForJobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachiningTools_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachiningTools_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartsForJobs",
                columns: table => new
                {
                    PartForWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    PartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolForJob_JobId",
                table: "MachiningToolForJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolForJob_UserId",
                table: "MachiningToolForJob",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolForWork_UserId",
                table: "MachiningToolForWork",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolForWork_WorkId",
                table: "MachiningToolForWork",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolForWorkOrder_UserId",
                table: "MachiningToolForWorkOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningToolForWorkOrder_WorkOrderId",
                table: "MachiningToolForWorkOrder",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningTools_ToolToJobMachiningToolForJobId",
                table: "MachiningTools",
                column: "ToolToJobMachiningToolForJobId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningTools_UserId",
                table: "MachiningTools",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MachiningTools_VendorId",
                table: "MachiningTools",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_UserId",
                table: "Parts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_WorkId",
                table: "Parts",
                column: "WorkId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Powders_UserId",
                table: "Powders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Powders_VendorId",
                table: "Powders",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EndlasEmail",
                table: "Users",
                column: "EndlasEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_CustomerId",
                table: "Work",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_UserId",
                table: "Work",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvironmentalSnapshots");

            migrationBuilder.DropTable(
                name: "MachiningToolForWork");

            migrationBuilder.DropTable(
                name: "MachiningToolForWorkOrder");

            migrationBuilder.DropTable(
                name: "MachiningTools");

            migrationBuilder.DropTable(
                name: "PartsForJobs");

            migrationBuilder.DropTable(
                name: "PartsForWorkOrders");

            migrationBuilder.DropTable(
                name: "Powders");

            migrationBuilder.DropTable(
                name: "MachiningToolForJob");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Work");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
