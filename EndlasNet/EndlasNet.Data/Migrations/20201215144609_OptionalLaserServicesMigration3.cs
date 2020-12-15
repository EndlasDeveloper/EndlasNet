using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class OptionalLaserServicesMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserService_OptionalLaserServicesId",
                table: "LaserQuoteSessions");

            migrationBuilder.AlterColumn<int>(
                name: "OptionalLaserServicesId",
                table: "LaserQuoteSessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserService_OptionalLaserServicesId",
                table: "LaserQuoteSessions",
                column: "OptionalLaserServicesId",
                principalTable: "OptionalLaserService",
                principalColumn: "OptionalLaserServicesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserService_OptionalLaserServicesId",
                table: "LaserQuoteSessions");

            migrationBuilder.AlterColumn<int>(
                name: "OptionalLaserServicesId",
                table: "LaserQuoteSessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserService_OptionalLaserServicesId",
                table: "LaserQuoteSessions",
                column: "OptionalLaserServicesId",
                principalTable: "OptionalLaserService",
                principalColumn: "OptionalLaserServicesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
