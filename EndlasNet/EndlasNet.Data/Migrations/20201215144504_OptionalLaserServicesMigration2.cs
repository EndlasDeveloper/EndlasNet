using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class OptionalLaserServicesMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserServices_OptionalLaserServicesId",
                table: "LaserQuoteSessions");

            migrationBuilder.DropTable(
                name: "OptionalLaserServices");

            migrationBuilder.CreateTable(
                name: "OptionalLaserService",
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
                    table.PrimaryKey("PK_OptionalLaserService", x => x.OptionalLaserServicesId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserService_OptionalLaserServicesId",
                table: "LaserQuoteSessions",
                column: "OptionalLaserServicesId",
                principalTable: "OptionalLaserService",
                principalColumn: "OptionalLaserServicesId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserService_OptionalLaserServicesId",
                table: "LaserQuoteSessions");

            migrationBuilder.DropTable(
                name: "OptionalLaserService");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserServices_OptionalLaserServicesId",
                table: "LaserQuoteSessions",
                column: "OptionalLaserServicesId",
                principalTable: "OptionalLaserServices",
                principalColumn: "OptionalLaserServicesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
