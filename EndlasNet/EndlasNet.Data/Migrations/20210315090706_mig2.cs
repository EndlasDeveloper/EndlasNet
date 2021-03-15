using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<float>(
                name: "PowderWeightUsed",
                table: "PowderForParts",
                type: "real",
                nullable: true);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
