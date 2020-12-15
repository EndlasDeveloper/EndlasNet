using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class RawMaterialEmpiricalMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlowRateSlope",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "FlowRateYIntercept",
                table: "RawMaterials");

            migrationBuilder.AddColumn<int>(
                name: "RawMaterialEmpiricalId",
                table: "RawMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RawMaterialEmpirical",
                columns: table => new
                {
                    RawMaterialEmpiricalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowRateSlope = table.Column<double>(type: "float", nullable: false),
                    FlowRateYIntercept = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterialEmpirical", x => x.RawMaterialEmpiricalId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterials_RawMaterialEmpiricalId",
                table: "RawMaterials",
                column: "RawMaterialEmpiricalId");

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterials_RawMaterialEmpirical_RawMaterialEmpiricalId",
                table: "RawMaterials",
                column: "RawMaterialEmpiricalId",
                principalTable: "RawMaterialEmpirical",
                principalColumn: "RawMaterialEmpiricalId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterials_RawMaterialEmpirical_RawMaterialEmpiricalId",
                table: "RawMaterials");

            migrationBuilder.DropTable(
                name: "RawMaterialEmpirical");

            migrationBuilder.DropIndex(
                name: "IX_RawMaterials_RawMaterialEmpiricalId",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "RawMaterialEmpiricalId",
                table: "RawMaterials");

            migrationBuilder.AddColumn<double>(
                name: "FlowRateSlope",
                table: "RawMaterials",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FlowRateYIntercept",
                table: "RawMaterials",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
