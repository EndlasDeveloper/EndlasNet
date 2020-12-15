using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class RemoveRedundantColumnIntermediateParamMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproxVolPerLayerCubicIn",
                table: "IntermediateParams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ApproxVolPerLayerCubicIn",
                table: "IntermediateParams",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
