using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class RawMaterial_LaserQuoteSessionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_RawMaterial_LaserQuoteSession_RawMaterialId",
                table: "RawMaterial_LaserQuoteSession",
                column: "RawMaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawMaterial_LaserQuoteSession");
        }
    }
}
