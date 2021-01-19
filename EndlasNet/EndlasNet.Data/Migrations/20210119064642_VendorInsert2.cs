using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class VendorInsert2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Vendors",
                newName: "VendorPhone");

            migrationBuilder.RenameColumn(
                name: "POC",
                table: "Vendors",
                newName: "VendorName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Vendors",
                newName: "VendorAddress");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Vendors",
                newName: "PointOfContact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VendorPhone",
                table: "Vendors",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "VendorName",
                table: "Vendors",
                newName: "POC");

            migrationBuilder.RenameColumn(
                name: "VendorAddress",
                table: "Vendors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PointOfContact",
                table: "Vendors",
                newName: "Address");
        }
    }
}
