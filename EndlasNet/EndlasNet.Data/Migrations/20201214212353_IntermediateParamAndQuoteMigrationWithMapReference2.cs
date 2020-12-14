using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class IntermediateParamAndQuoteMigrationWithMapReference2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntermediateParam_LaserQuoteSession_LaserQuoteSessionId",
                table: "IntermediateParam");

            migrationBuilder.DropForeignKey(
                name: "FK_LaserQuoteSession_QuoteSessions_QuoteSessionId",
                table: "LaserQuoteSession");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineQuoteSession_QuoteSessions_QuoteSessionId",
                table: "MachineQuoteSession");

            migrationBuilder.DropForeignKey(
                name: "FK_Quote_LaserQuoteSession_LaserQuoteSessionId",
                table: "Quote");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterial_LaserQuoteSession_LaserQuoteSession_LaserQuoteSessionId",
                table: "RawMaterial_LaserQuoteSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quote",
                table: "Quote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MachineQuoteSession",
                table: "MachineQuoteSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LaserQuoteSession",
                table: "LaserQuoteSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntermediateParam",
                table: "IntermediateParam");

            migrationBuilder.RenameTable(
                name: "Quote",
                newName: "Quotes");

            migrationBuilder.RenameTable(
                name: "MachineQuoteSession",
                newName: "MachineSessions");

            migrationBuilder.RenameTable(
                name: "LaserQuoteSession",
                newName: "LaserQuoteSessions");

            migrationBuilder.RenameTable(
                name: "IntermediateParam",
                newName: "IntermediateParams");

            migrationBuilder.RenameIndex(
                name: "IX_Quote_LaserQuoteSessionId",
                table: "Quotes",
                newName: "IX_Quotes_LaserQuoteSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_MachineQuoteSession_QuoteSessionId",
                table: "MachineSessions",
                newName: "IX_MachineSessions_QuoteSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_LaserQuoteSession_QuoteSessionId",
                table: "LaserQuoteSessions",
                newName: "IX_LaserQuoteSessions_QuoteSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_IntermediateParam_LaserQuoteSessionId",
                table: "IntermediateParams",
                newName: "IX_IntermediateParams_LaserQuoteSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quotes",
                table: "Quotes",
                column: "QuoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MachineSessions",
                table: "MachineSessions",
                column: "MachineQuoteSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LaserQuoteSessions",
                table: "LaserQuoteSessions",
                column: "LaserQuoteSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntermediateParams",
                table: "IntermediateParams",
                column: "IntermediateParamId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntermediateParams_LaserQuoteSessions_LaserQuoteSessionId",
                table: "IntermediateParams",
                column: "LaserQuoteSessionId",
                principalTable: "LaserQuoteSessions",
                principalColumn: "LaserQuoteSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaserQuoteSessions_QuoteSessions_QuoteSessionId",
                table: "LaserQuoteSessions",
                column: "QuoteSessionId",
                principalTable: "QuoteSessions",
                principalColumn: "QuoteSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSessions_QuoteSessions_QuoteSessionId",
                table: "MachineSessions",
                column: "QuoteSessionId",
                principalTable: "QuoteSessions",
                principalColumn: "QuoteSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_LaserQuoteSessions_LaserQuoteSessionId",
                table: "Quotes",
                column: "LaserQuoteSessionId",
                principalTable: "LaserQuoteSessions",
                principalColumn: "LaserQuoteSessionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterial_LaserQuoteSession_LaserQuoteSessions_LaserQuoteSessionId",
                table: "RawMaterial_LaserQuoteSession",
                column: "LaserQuoteSessionId",
                principalTable: "LaserQuoteSessions",
                principalColumn: "LaserQuoteSessionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntermediateParams_LaserQuoteSessions_LaserQuoteSessionId",
                table: "IntermediateParams");

            migrationBuilder.DropForeignKey(
                name: "FK_LaserQuoteSessions_QuoteSessions_QuoteSessionId",
                table: "LaserQuoteSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineSessions_QuoteSessions_QuoteSessionId",
                table: "MachineSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_LaserQuoteSessions_LaserQuoteSessionId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterial_LaserQuoteSession_LaserQuoteSessions_LaserQuoteSessionId",
                table: "RawMaterial_LaserQuoteSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quotes",
                table: "Quotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MachineSessions",
                table: "MachineSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LaserQuoteSessions",
                table: "LaserQuoteSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntermediateParams",
                table: "IntermediateParams");

            migrationBuilder.RenameTable(
                name: "Quotes",
                newName: "Quote");

            migrationBuilder.RenameTable(
                name: "MachineSessions",
                newName: "MachineQuoteSession");

            migrationBuilder.RenameTable(
                name: "LaserQuoteSessions",
                newName: "LaserQuoteSession");

            migrationBuilder.RenameTable(
                name: "IntermediateParams",
                newName: "IntermediateParam");

            migrationBuilder.RenameIndex(
                name: "IX_Quotes_LaserQuoteSessionId",
                table: "Quote",
                newName: "IX_Quote_LaserQuoteSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_MachineSessions_QuoteSessionId",
                table: "MachineQuoteSession",
                newName: "IX_MachineQuoteSession_QuoteSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_LaserQuoteSessions_QuoteSessionId",
                table: "LaserQuoteSession",
                newName: "IX_LaserQuoteSession_QuoteSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_IntermediateParams_LaserQuoteSessionId",
                table: "IntermediateParam",
                newName: "IX_IntermediateParam_LaserQuoteSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quote",
                table: "Quote",
                column: "QuoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MachineQuoteSession",
                table: "MachineQuoteSession",
                column: "MachineQuoteSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LaserQuoteSession",
                table: "LaserQuoteSession",
                column: "LaserQuoteSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntermediateParam",
                table: "IntermediateParam",
                column: "IntermediateParamId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntermediateParam_LaserQuoteSession_LaserQuoteSessionId",
                table: "IntermediateParam",
                column: "LaserQuoteSessionId",
                principalTable: "LaserQuoteSession",
                principalColumn: "LaserQuoteSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaserQuoteSession_QuoteSessions_QuoteSessionId",
                table: "LaserQuoteSession",
                column: "QuoteSessionId",
                principalTable: "QuoteSessions",
                principalColumn: "QuoteSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineQuoteSession_QuoteSessions_QuoteSessionId",
                table: "MachineQuoteSession",
                column: "QuoteSessionId",
                principalTable: "QuoteSessions",
                principalColumn: "QuoteSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quote_LaserQuoteSession_LaserQuoteSessionId",
                table: "Quote",
                column: "LaserQuoteSessionId",
                principalTable: "LaserQuoteSession",
                principalColumn: "LaserQuoteSessionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterial_LaserQuoteSession_LaserQuoteSession_LaserQuoteSessionId",
                table: "RawMaterial_LaserQuoteSession",
                column: "LaserQuoteSessionId",
                principalTable: "LaserQuoteSession",
                principalColumn: "LaserQuoteSessionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
