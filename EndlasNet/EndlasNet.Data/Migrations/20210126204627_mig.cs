using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Vendors",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Inserts",
                newName: "UpdatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Vendors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Inserts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Inserts");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Vendors",
                newName: "DateAdded");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Inserts",
                newName: "DateAdded");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
