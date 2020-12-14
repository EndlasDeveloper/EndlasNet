using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class IntermediateParamAndQuoteMigrationWithMapReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntermediateParam",
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
                    table.PrimaryKey("PK_IntermediateParam", x => x.IntermediateParamId);
                    table.ForeignKey(
                        name: "FK_IntermediateParam_LaserQuoteSession_LaserQuoteSessionId",
                        column: x => x.LaserQuoteSessionId,
                        principalTable: "LaserQuoteSession",
                        principalColumn: "LaserQuoteSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quote",
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
                    LaserCladSessionId = table.Column<int>(type: "int", nullable: false),
                    LaserQuoteSessionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote", x => x.QuoteId);
                    table.ForeignKey(
                        name: "FK_Quote_LaserQuoteSession_LaserQuoteSessionId",
                        column: x => x.LaserQuoteSessionId,
                        principalTable: "LaserQuoteSession",
                        principalColumn: "LaserQuoteSessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntermediateParam_LaserQuoteSessionId",
                table: "IntermediateParam",
                column: "LaserQuoteSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_LaserQuoteSessionId",
                table: "Quote",
                column: "LaserQuoteSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntermediateParam");

            migrationBuilder.DropTable(
                name: "Quote");
        }
    }
}
