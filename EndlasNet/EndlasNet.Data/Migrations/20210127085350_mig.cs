using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
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
                name: "ToolToJob",
                columns: table => new
                {
                    ToolToJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateUsed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolToJob", x => x.ToolToJobId);
                    table.ForeignKey(
                        name: "FK_ToolToJob_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToolToJob_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "DrillBits",
                columns: table => new
                {
                    MachiningToolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DrillBitRadius = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VendorDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ToolCount = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNum = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PurchaseOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseOrderPrice = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToolToJobsToolToJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrillBits", x => x.MachiningToolId);
                    table.ForeignKey(
                        name: "FK_DrillBits_ToolToJob_ToolToJobsToolToJobId",
                        column: x => x.ToolToJobsToolToJobId,
                        principalTable: "ToolToJob",
                        principalColumn: "ToolToJobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrillBits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrillBits_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inserts",
                columns: table => new
                {
                    MachiningToolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToolTipRadius = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VendorDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ToolCount = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderNum = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PurchaseOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseOrderPrice = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToolToJobsToolToJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inserts", x => x.MachiningToolId);
                    table.ForeignKey(
                        name: "FK_Inserts_ToolToJob_ToolToJobsToolToJobId",
                        column: x => x.ToolToJobsToolToJobId,
                        principalTable: "ToolToJob",
                        principalColumn: "ToolToJobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inserts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inserts_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("0d9329ca-2f88-40fe-b7cc-d08af75c85f4"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "James.Tomich@endlas.com", "James", "Tomich" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("d3e520f5-f3b9-43ac-ad2a-1b3ba691863e"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "Josh.Hammell@endlas.com", "Josh", "Hammell" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("38c9c455-eef2-4163-b82f-c127fe7b0282"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "Brett.Trotter@endlas.com", "Brett", "Trotter" });

            migrationBuilder.CreateIndex(
                name: "IX_DrillBits_ToolToJobsToolToJobId",
                table: "DrillBits",
                column: "ToolToJobsToolToJobId");

            migrationBuilder.CreateIndex(
                name: "IX_DrillBits_UserId",
                table: "DrillBits",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DrillBits_VendorId",
                table: "DrillBits",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Inserts_ToolToJobsToolToJobId",
                table: "Inserts",
                column: "ToolToJobsToolToJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Inserts_UserId",
                table: "Inserts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inserts_VendorId",
                table: "Inserts",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolToJob_JobId",
                table: "ToolToJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolToJob_UserId",
                table: "ToolToJob",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EndlasEmail",
                table: "Users",
                column: "EndlasEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrillBits");

            migrationBuilder.DropTable(
                name: "Inserts");

            migrationBuilder.DropTable(
                name: "ToolToJob");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
