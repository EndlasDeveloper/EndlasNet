using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class LaserQuoteSessionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LaserQuoteSession",
                columns: table => new
                {
                    LaserQuoteSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaserQuoteSession", x => x.LaserQuoteSessionId);
                    table.ForeignKey(
                        name: "FK_LaserQuoteSession_QuoteSessions_QuoteSessionId",
                        column: x => x.QuoteSessionId,
                        principalTable: "QuoteSessions",
                        principalColumn: "QuoteSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LaserQuoteSession_QuoteSessionId",
                table: "LaserQuoteSession",
                column: "QuoteSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaserQuoteSession");
        }
    }
}
