using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class OptionalLaserServicesMigration : Migration
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
                name: "OptionalLaserServices",
                columns: table => new
                {
                    OptionalLaserServicesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeatTreatedBlankWt = table.Column<double>(type: "float", nullable: false),
                    HeatTreatedPricePerLb = table.Column<double>(type: "float", nullable: false),
                    MinHeatTreatmentPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionalLaserServices", x => x.OptionalLaserServicesId);
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
                name: "LaserQuoteSessions",
                columns: table => new
                {
                    LaserQuoteSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsFlowRateAnalytical = table.Column<bool>(type: "bit", nullable: false),
                    FinishedPartWeight = table.Column<double>(type: "float", nullable: false),
                    NumLayers = table.Column<int>(type: "int", nullable: false),
                    NumParts = table.Column<int>(type: "int", nullable: false),
                    PartChangeoverTimeHr = table.Column<double>(type: "float", nullable: false),
                    PartSurfaceAreaSqIn = table.Column<double>(type: "float", nullable: false),
                    SetupTimeMin = table.Column<double>(type: "float", nullable: false),
                    ShippingWeightFactor = table.Column<double>(type: "float", nullable: false),
                    ArgonCost = table.Column<double>(type: "float", nullable: false),
                    EstPowerCost = table.Column<double>(type: "float", nullable: false),
                    HourlyLaborRate = table.Column<double>(type: "float", nullable: false),
                    HourlyUseRate = table.Column<double>(type: "float", nullable: false),
                    FringeRate = table.Column<double>(type: "float", nullable: false),
                    ProfitRate = table.Column<double>(type: "float", nullable: false),
                    OverheadRate = table.Column<double>(type: "float", nullable: false),
                    QuoteSessionId = table.Column<int>(type: "int", nullable: false),
                    OptionalLaserServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaserQuoteSessions", x => x.LaserQuoteSessionId);
                    table.ForeignKey(
                        name: "FK_LaserQuoteSessions_OptionalLaserServices_OptionalLaserServicesId",
                        column: x => x.OptionalLaserServicesId,
                        principalTable: "OptionalLaserServices",
                        principalColumn: "OptionalLaserServicesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaserQuoteSessions_QuoteSessions_QuoteSessionId",
                        column: x => x.QuoteSessionId,
                        principalTable: "QuoteSessions",
                        principalColumn: "QuoteSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineSessions",
                columns: table => new
                {
                    MachineQuoteSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineSessions", x => x.MachineQuoteSessionId);
                    table.ForeignKey(
                        name: "FK_MachineSessions_QuoteSessions_QuoteSessionId",
                        column: x => x.QuoteSessionId,
                        principalTable: "QuoteSessions",
                        principalColumn: "QuoteSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    QuoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PowderDirectTotal = table.Column<double>(type: "float", nullable: false),
                    GasTotal = table.Column<double>(type: "float", nullable: false),
                    EnergyTotal = table.Column<double>(type: "float", nullable: false),
                    ShippingTotal = table.Column<double>(type: "float", nullable: false),
                    CogsTotal = table.Column<double>(type: "float", nullable: false),
                    LaborDirectTotal = table.Column<double>(type: "float", nullable: false),
                    FringeTotal = table.Column<double>(type: "float", nullable: false),
                    ProfitTotal = table.Column<double>(type: "float", nullable: false),
                    OverheadTotal = table.Column<double>(type: "float", nullable: false),
                    QuoteSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.QuoteId);
                    table.ForeignKey(
                        name: "FK_Quotes_QuoteSessions_QuoteSessionId",
                        column: x => x.QuoteSessionId,
                        principalTable: "QuoteSessions",
                        principalColumn: "QuoteSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntermediateParams",
                columns: table => new
                {
                    IntermediateParamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurfaceVelocity = table.Column<double>(type: "float", nullable: false),
                    StepMm = table.Column<double>(type: "float", nullable: false),
                    StepIn = table.Column<double>(type: "float", nullable: false),
                    AssumedAvgPassLenIn = table.Column<double>(type: "float", nullable: false),
                    PseudoWidthIn = table.Column<double>(type: "float", nullable: false),
                    PseudoNumPasses = table.Column<int>(type: "int", nullable: false),
                    TimePerBeadSec = table.Column<double>(type: "float", nullable: false),
                    TimeBetweenBeadsMin = table.Column<double>(type: "float", nullable: false),
                    TimePerLayerMin = table.Column<double>(type: "float", nullable: false),
                    CladAddRateSqInPerMin = table.Column<double>(type: "float", nullable: false),
                    ApproxVolPerLayerCubicIn = table.Column<double>(type: "float", nullable: false),
                    ApproxVolPerLayerCubicCm = table.Column<double>(type: "float", nullable: false),
                    LaserQuoteSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntermediateParams", x => x.IntermediateParamId);
                    table.ForeignKey(
                        name: "FK_IntermediateParams_LaserQuoteSessions_LaserQuoteSessionId",
                        column: x => x.LaserQuoteSessionId,
                        principalTable: "LaserQuoteSessions",
                        principalColumn: "LaserQuoteSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RawMaterial_LaserQuoteSession",
                columns: table => new
                {
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    LaserQuoteSessionId = table.Column<int>(type: "int", nullable: false),
                    PowerInWatts = table.Column<double>(type: "float", nullable: false),
                    AvgThicknessIn = table.Column<double>(type: "float", nullable: false),
                    SpotSizeMm = table.Column<double>(type: "float", nullable: false),
                    PercentBeadOverlap = table.Column<double>(type: "float", nullable: false),
                    SurfaceVelocityMmPerSec = table.Column<double>(type: "float", nullable: false),
                    PowderRpm = table.Column<double>(type: "float", nullable: false),
                    EstCaptureEffeciency = table.Column<double>(type: "float", nullable: false),
                    ProcessingFlowRateLiPerMin = table.Column<double>(type: "float", nullable: false),
                    LayerSurfaceAreaSqIn = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterial_LaserQuoteSession", x => new { x.LaserQuoteSessionId, x.RawMaterialId });
                    table.ForeignKey(
                        name: "FK_RawMaterial_LaserQuoteSession_LaserQuoteSessions_LaserQuoteSessionId",
                        column: x => x.LaserQuoteSessionId,
                        principalTable: "LaserQuoteSessions",
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
                name: "IX_IntermediateParams_LaserQuoteSessionId",
                table: "IntermediateParams",
                column: "LaserQuoteSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_LaserQuoteSessions_OptionalLaserServicesId",
                table: "LaserQuoteSessions",
                column: "OptionalLaserServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_LaserQuoteSessions_QuoteSessionId",
                table: "LaserQuoteSessions",
                column: "QuoteSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSessions_QuoteSessionId",
                table: "MachineSessions",
                column: "QuoteSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_QuoteSessionId",
                table: "Quotes",
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
                name: "IntermediateParams");

            migrationBuilder.DropTable(
                name: "MachineSessions");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "RawMaterial_LaserQuoteSession");

            migrationBuilder.DropTable(
                name: "LaserQuoteSessions");

            migrationBuilder.DropTable(
                name: "RawMaterials");

            migrationBuilder.DropTable(
                name: "OptionalLaserServices");

            migrationBuilder.DropTable(
                name: "QuoteSessions");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
