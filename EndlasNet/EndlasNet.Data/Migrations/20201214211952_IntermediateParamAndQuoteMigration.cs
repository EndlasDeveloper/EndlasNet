using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class IntermediateParamAndQuoteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointOfContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "RawMaterials",
                columns: table => new
                {
                    RawMaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PowderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PowderLayerPrice = table.Column<double>(type: "float", nullable: false),
                    CladLayerDensity = table.Column<double>(type: "float", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PowderFeeder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlowRateSlope = table.Column<double>(type: "float", nullable: false),
                    FlowRateYIntercept = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterials", x => x.RawMaterialId);
                });

            migrationBuilder.CreateTable(
                name: "QuoteSessions",
                columns: table => new
                {
                    QuoteSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteSessionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuoteSessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteSessions", x => x.QuoteSessionId);
                    table.ForeignKey(
                        name: "FK_QuoteSessions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaserQuoteSession",
                columns: table => new
                {
                    LaserQuoteSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaserQuoteSession", x => x.LaserQuoteSessionId);
                    table.ForeignKey(
                        name: "FK_LaserQuoteSession_QuoteSessions_QuoteSessionId",
                        column: x => x.QuoteSessionId,
                        principalTable: "QuoteSessions",
                        principalColumn: "QuoteSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineQuoteSession",
                columns: table => new
                {
                    MachineQuoteSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineQuoteSession", x => x.MachineQuoteSessionId);
                    table.ForeignKey(
                        name: "FK_MachineQuoteSession_QuoteSessions_QuoteSessionId",
                        column: x => x.QuoteSessionId,
                        principalTable: "QuoteSessions",
                        principalColumn: "QuoteSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RawMaterial_LaserQuoteSession",
                columns: table => new
                {
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    LaserQuoteSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterial_LaserQuoteSession", x => new { x.LaserQuoteSessionId, x.RawMaterialId });
                    table.ForeignKey(
                        name: "FK_RawMaterial_LaserQuoteSession_LaserQuoteSession_LaserQuoteSessionId",
                        column: x => x.LaserQuoteSessionId,
                        principalTable: "LaserQuoteSession",
                        principalColumn: "LaserQuoteSessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RawMaterial_LaserQuoteSession_RawMaterials_RawMaterialId",
                        column: x => x.RawMaterialId,
                        principalTable: "RawMaterials",
                        principalColumn: "RawMaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LaserQuoteSession_QuoteSessionId",
                table: "LaserQuoteSession",
                column: "QuoteSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineQuoteSession_QuoteSessionId",
                table: "MachineQuoteSession",
                column: "QuoteSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteSessions_CustomerId",
                table: "QuoteSessions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterial_LaserQuoteSession_RawMaterialId",
                table: "RawMaterial_LaserQuoteSession",
                column: "RawMaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineQuoteSession");

            migrationBuilder.DropTable(
                name: "RawMaterial_LaserQuoteSession");

            migrationBuilder.DropTable(
                name: "LaserQuoteSession");

            migrationBuilder.DropTable(
                name: "RawMaterials");

            migrationBuilder.DropTable(
                name: "QuoteSessions");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
