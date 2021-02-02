using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class migg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnvironmentalSnapshot_Job_EnvironmentalSnapshots_EnvSnapshotId",
                table: "EnvironmentalSnapshot_Job");

            migrationBuilder.DropForeignKey(
                name: "FK_EnvironmentalSnapshot_Job_Jobs_JobId",
                table: "EnvironmentalSnapshot_Job");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnvironmentalSnapshot_Job",
                table: "EnvironmentalSnapshot_Job");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6447bc86-8afb-4318-8efa-df4e0c32aea1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("79585a47-9367-4469-b8ba-e189e7678c1c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d448133a-905f-4c37-a6c3-7788e803162b"));

            migrationBuilder.RenameTable(
                name: "EnvironmentalSnapshot_Job",
                newName: "EnvironmentalSnapshot_Jobs");

            migrationBuilder.RenameIndex(
                name: "IX_EnvironmentalSnapshot_Job_JobId",
                table: "EnvironmentalSnapshot_Jobs",
                newName: "IX_EnvironmentalSnapshot_Jobs_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnvironmentalSnapshot_Jobs",
                table: "EnvironmentalSnapshot_Jobs",
                columns: new[] { "EnvSnapshotId", "JobId" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("37184d4a-b832-424b-b7f6-c54474bade94"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "James.Tomich@endlas.com", "James", "Tomich" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("8c6f4486-ffb1-4ceb-93d2-40c71e595380"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "Josh.Hammell@endlas.com", "Josh", "Hammell" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("bcf207ef-2960-4f05-8ad0-84986ace5697"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "BLT@endlas.com", "Brett", "Trotter" });

            migrationBuilder.AddForeignKey(
                name: "FK_EnvironmentalSnapshot_Jobs_EnvironmentalSnapshots_EnvSnapshotId",
                table: "EnvironmentalSnapshot_Jobs",
                column: "EnvSnapshotId",
                principalTable: "EnvironmentalSnapshots",
                principalColumn: "EnvSnapshotId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnvironmentalSnapshot_Jobs_Jobs_JobId",
                table: "EnvironmentalSnapshot_Jobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnvironmentalSnapshot_Jobs_EnvironmentalSnapshots_EnvSnapshotId",
                table: "EnvironmentalSnapshot_Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_EnvironmentalSnapshot_Jobs_Jobs_JobId",
                table: "EnvironmentalSnapshot_Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnvironmentalSnapshot_Jobs",
                table: "EnvironmentalSnapshot_Jobs");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("37184d4a-b832-424b-b7f6-c54474bade94"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8c6f4486-ffb1-4ceb-93d2-40c71e595380"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bcf207ef-2960-4f05-8ad0-84986ace5697"));

            migrationBuilder.RenameTable(
                name: "EnvironmentalSnapshot_Jobs",
                newName: "EnvironmentalSnapshot_Job");

            migrationBuilder.RenameIndex(
                name: "IX_EnvironmentalSnapshot_Jobs_JobId",
                table: "EnvironmentalSnapshot_Job",
                newName: "IX_EnvironmentalSnapshot_Job_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnvironmentalSnapshot_Job",
                table: "EnvironmentalSnapshot_Job",
                columns: new[] { "EnvSnapshotId", "JobId" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("79585a47-9367-4469-b8ba-e189e7678c1c"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "James.Tomich@endlas.com", "James", "Tomich" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("d448133a-905f-4c37-a6c3-7788e803162b"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "Josh.Hammell@endlas.com", "Josh", "Hammell" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("6447bc86-8afb-4318-8efa-df4e0c32aea1"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "BLT@endlas.com", "Brett", "Trotter" });

            migrationBuilder.AddForeignKey(
                name: "FK_EnvironmentalSnapshot_Job_EnvironmentalSnapshots_EnvSnapshotId",
                table: "EnvironmentalSnapshot_Job",
                column: "EnvSnapshotId",
                principalTable: "EnvironmentalSnapshots",
                principalColumn: "EnvSnapshotId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnvironmentalSnapshot_Job_Jobs_JobId",
                table: "EnvironmentalSnapshot_Job",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
