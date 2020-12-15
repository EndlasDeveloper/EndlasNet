using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class AddedAllDbSetsMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserService_OptionalLaserServicesId",
                table: "LaserQuoteSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterial_LaserQuoteSession_LaserQuoteSessions_LaserQuoteSessionId",
                table: "RawMaterial_LaserQuoteSession");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterial_LaserQuoteSession_RawMaterials_RawMaterialId",
                table: "RawMaterial_LaserQuoteSession");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterials_RawMaterialEmpirical_RawMaterialEmpiricalId",
                table: "RawMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RawMaterialEmpirical",
                table: "RawMaterialEmpirical");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RawMaterial_LaserQuoteSession",
                table: "RawMaterial_LaserQuoteSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionalLaserService",
                table: "OptionalLaserService");

            migrationBuilder.RenameTable(
                name: "RawMaterialEmpirical",
                newName: "RawMaterialEmpiricals");

            migrationBuilder.RenameTable(
                name: "RawMaterial_LaserQuoteSession",
                newName: "RawMaterial_LaserQuoteSessions");

            migrationBuilder.RenameTable(
                name: "OptionalLaserService",
                newName: "OptionalLaserServices");

            migrationBuilder.RenameIndex(
                name: "IX_RawMaterial_LaserQuoteSession_RawMaterialId",
                table: "RawMaterial_LaserQuoteSessions",
                newName: "IX_RawMaterial_LaserQuoteSessions_RawMaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RawMaterialEmpiricals",
                table: "RawMaterialEmpiricals",
                column: "RawMaterialEmpiricalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RawMaterial_LaserQuoteSessions",
                table: "RawMaterial_LaserQuoteSessions",
                columns: new[] { "LaserQuoteSessionId", "RawMaterialId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionalLaserServices",
                table: "OptionalLaserServices",
                column: "OptionalLaserServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserServices_OptionalLaserServicesId",
                table: "LaserQuoteSessions",
                column: "OptionalLaserServicesId",
                principalTable: "OptionalLaserServices",
                principalColumn: "OptionalLaserServicesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterial_LaserQuoteSessions_LaserQuoteSessions_LaserQuoteSessionId",
                table: "RawMaterial_LaserQuoteSessions",
                column: "LaserQuoteSessionId",
                principalTable: "LaserQuoteSessions",
                principalColumn: "LaserQuoteSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterial_LaserQuoteSessions_RawMaterials_RawMaterialId",
                table: "RawMaterial_LaserQuoteSessions",
                column: "RawMaterialId",
                principalTable: "RawMaterials",
                principalColumn: "RawMaterialId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterials_RawMaterialEmpiricals_RawMaterialEmpiricalId",
                table: "RawMaterials",
                column: "RawMaterialEmpiricalId",
                principalTable: "RawMaterialEmpiricals",
                principalColumn: "RawMaterialEmpiricalId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserServices_OptionalLaserServicesId",
                table: "LaserQuoteSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterial_LaserQuoteSessions_LaserQuoteSessions_LaserQuoteSessionId",
                table: "RawMaterial_LaserQuoteSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterial_LaserQuoteSessions_RawMaterials_RawMaterialId",
                table: "RawMaterial_LaserQuoteSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterials_RawMaterialEmpiricals_RawMaterialEmpiricalId",
                table: "RawMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RawMaterialEmpiricals",
                table: "RawMaterialEmpiricals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RawMaterial_LaserQuoteSessions",
                table: "RawMaterial_LaserQuoteSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionalLaserServices",
                table: "OptionalLaserServices");

            migrationBuilder.RenameTable(
                name: "RawMaterialEmpiricals",
                newName: "RawMaterialEmpirical");

            migrationBuilder.RenameTable(
                name: "RawMaterial_LaserQuoteSessions",
                newName: "RawMaterial_LaserQuoteSession");

            migrationBuilder.RenameTable(
                name: "OptionalLaserServices",
                newName: "OptionalLaserService");

            migrationBuilder.RenameIndex(
                name: "IX_RawMaterial_LaserQuoteSessions_RawMaterialId",
                table: "RawMaterial_LaserQuoteSession",
                newName: "IX_RawMaterial_LaserQuoteSession_RawMaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RawMaterialEmpirical",
                table: "RawMaterialEmpirical",
                column: "RawMaterialEmpiricalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RawMaterial_LaserQuoteSession",
                table: "RawMaterial_LaserQuoteSession",
                columns: new[] { "LaserQuoteSessionId", "RawMaterialId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionalLaserService",
                table: "OptionalLaserService",
                column: "OptionalLaserServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaserQuoteSessions_OptionalLaserService_OptionalLaserServicesId",
                table: "LaserQuoteSessions",
                column: "OptionalLaserServicesId",
                principalTable: "OptionalLaserService",
                principalColumn: "OptionalLaserServicesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterial_LaserQuoteSession_LaserQuoteSessions_LaserQuoteSessionId",
                table: "RawMaterial_LaserQuoteSession",
                column: "LaserQuoteSessionId",
                principalTable: "LaserQuoteSessions",
                principalColumn: "LaserQuoteSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterial_LaserQuoteSession_RawMaterials_RawMaterialId",
                table: "RawMaterial_LaserQuoteSession",
                column: "RawMaterialId",
                principalTable: "RawMaterials",
                principalColumn: "RawMaterialId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterials_RawMaterialEmpirical_RawMaterialEmpiricalId",
                table: "RawMaterials",
                column: "RawMaterialEmpiricalId",
                principalTable: "RawMaterialEmpirical",
                principalColumn: "RawMaterialEmpiricalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
