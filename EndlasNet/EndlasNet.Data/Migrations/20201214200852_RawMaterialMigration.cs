using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class RawMaterialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawMaterials");
        }
    }
}
