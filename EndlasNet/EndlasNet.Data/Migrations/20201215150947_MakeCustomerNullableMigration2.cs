using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class MakeCustomerNullableMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteSessions_Customers_CustomerId",
                table: "QuoteSessions");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "QuoteSessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteSessions_Customers_CustomerId",
                table: "QuoteSessions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteSessions_Customers_CustomerId",
                table: "QuoteSessions");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "QuoteSessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteSessions_Customers_CustomerId",
                table: "QuoteSessions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
