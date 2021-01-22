using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "InsertToJobs",
                columns: table => new
                {
                    InsertToJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateUsed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsertToJobs", x => x.InsertToJobId);
                    table.ForeignKey(
                        name: "FK_InsertToJobs_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsertToJobs_Users_UserId",
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
                    VendorPhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "Inserts",
                columns: table => new
                {
                    InsertId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    InsertCount = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseOrderNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseOrderPrice = table.Column<float>(type: "real", nullable: false),
                    ToolTipRadius = table.Column<float>(type: "real", nullable: false),
                    VendorPartNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertToJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inserts", x => x.InsertId);
                    table.ForeignKey(
                        name: "FK_Inserts_InsertToJobs_InsertToJobId",
                        column: x => x.InsertToJobId,
                        principalTable: "InsertToJobs",
                        principalColumn: "InsertToJobId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Inserts_InsertToJobId",
                table: "Inserts",
                column: "InsertToJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Inserts_UserId",
                table: "Inserts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inserts_VendorId",
                table: "Inserts",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_InsertToJobs_JobId",
                table: "InsertToJobs",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_InsertToJobs_UserId",
                table: "InsertToJobs",
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
                name: "Inserts");

            migrationBuilder.DropTable(
                name: "InsertToJobs");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
