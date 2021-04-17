using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PowderBottles_BottleNumber",
                table: "PowderBottles");

            migrationBuilder.AlterColumn<string>(
                name: "BottleNumber",
                table: "PowderBottles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.AlterColumn<string>(
                name: "BottleNumber",
                table: "PowderBottles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

       

            migrationBuilder.CreateIndex(
                name: "IX_PowderBottles_BottleNumber",
                table: "PowderBottles",
                column: "BottleNumber",
                unique: true);
        }
    }
}
