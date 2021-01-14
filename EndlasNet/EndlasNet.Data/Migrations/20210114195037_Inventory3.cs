using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class Inventory3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inserts_Vendors_VendorId",
                table: "Inserts");

            migrationBuilder.DropForeignKey(
                name: "FK_InsertToJobs_Jobs_JobId",
                table: "InsertToJobs");

            migrationBuilder.DropColumn(
                name: "FKEmployeeId",
                table: "InsertToJobs");

            migrationBuilder.DropColumn(
                name: "FKInsertToJobId",
                table: "Inserts");

            migrationBuilder.DropColumn(
                name: "FKVendorId",
                table: "Inserts");

            migrationBuilder.RenameColumn(
                name: "FKJobId",
                table: "InsertToJobs",
                newName: "EmployeeId");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "InsertToJobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "Inserts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsertToJobs_EmployeeId",
                table: "InsertToJobs",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inserts_Vendors_VendorId",
                table: "Inserts",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsertToJobs_Employees_EmployeeId",
                table: "InsertToJobs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsertToJobs_Jobs_JobId",
                table: "InsertToJobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inserts_Vendors_VendorId",
                table: "Inserts");

            migrationBuilder.DropForeignKey(
                name: "FK_InsertToJobs_Employees_EmployeeId",
                table: "InsertToJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_InsertToJobs_Jobs_JobId",
                table: "InsertToJobs");

            migrationBuilder.DropIndex(
                name: "IX_InsertToJobs_EmployeeId",
                table: "InsertToJobs");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "InsertToJobs",
                newName: "FKJobId");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "InsertToJobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FKEmployeeId",
                table: "InsertToJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "Inserts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FKInsertToJobId",
                table: "Inserts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKVendorId",
                table: "Inserts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Inserts_Vendors_VendorId",
                table: "Inserts",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InsertToJobs_Jobs_JobId",
                table: "InsertToJobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
