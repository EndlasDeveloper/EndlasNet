using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class MachineQuoteSessionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineQuoteSession",
                columns: table => new
                {
                    MachineQuoteSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineQuoteSession", x => x.MachineQuoteSessionId);
                    table.ForeignKey(
                        name: "FK_MachineQuoteSession_QuoteSessions_QuoteSessionId",
                        column: x => x.QuoteSessionId,
                        principalTable: "QuoteSessions",
                        principalColumn: "QuoteSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineQuoteSession_QuoteSessionId",
                table: "MachineQuoteSession",
                column: "QuoteSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineQuoteSession");
        }
    }
}
