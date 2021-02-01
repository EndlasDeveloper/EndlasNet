using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnvironmentalSnapshot_Jobs_JobId",
                table: "EnvironmentalSnapshot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnvironmentalSnapshot",
                table: "EnvironmentalSnapshot");

            migrationBuilder.DropIndex(
                name: "IX_EnvironmentalSnapshot_JobId",
                table: "EnvironmentalSnapshot");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("07fb92c3-8739-4725-8891-52174938994b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("2b232a04-2e1d-47ff-a75b-21459232ce02"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8a280aa8-ca07-4595-aa18-715d75daaa6e"));

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "EnvironmentalSnapshot");

            migrationBuilder.RenameTable(
                name: "EnvironmentalSnapshot",
                newName: "EnvironmentalSnapshots");

            migrationBuilder.RenameColumn(
                name: "SnapshotDateTime",
                table: "EnvironmentalSnapshots",
                newName: "DateTimeCollected");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnvironmentalSnapshots",
                table: "EnvironmentalSnapshots",
                column: "EnvSnapshotId");

            migrationBuilder.CreateTable(
                name: "EnvironmentalSnapshot_Job",
                columns: table => new
                {
                    EnvSnapshotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSnapshot_Job", x => new { x.EnvSnapshotId, x.JobId });
                    table.ForeignKey(
                        name: "FK_EnvironmentalSnapshot_Job_EnvironmentalSnapshots_EnvSnapshotId",
                        column: x => x.EnvSnapshotId,
                        principalTable: "EnvironmentalSnapshots",
                        principalColumn: "EnvSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnvironmentalSnapshot_Job_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalSnapshot_Job_JobId",
                table: "EnvironmentalSnapshot_Job",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvironmentalSnapshot_Job");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnvironmentalSnapshots",
                table: "EnvironmentalSnapshots");

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
                name: "EnvironmentalSnapshots",
                newName: "EnvironmentalSnapshot");

            migrationBuilder.RenameColumn(
                name: "DateTimeCollected",
                table: "EnvironmentalSnapshot",
                newName: "SnapshotDateTime");

            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "EnvironmentalSnapshot",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnvironmentalSnapshot",
                table: "EnvironmentalSnapshot",
                column: "EnvSnapshotId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("8a280aa8-ca07-4595-aa18-715d75daaa6e"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "James.Tomich@endlas.com", "James", "Tomich" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("07fb92c3-8739-4725-8891-52174938994b"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "Josh.Hammell@endlas.com", "Josh", "Hammell" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("2b232a04-2e1d-47ff-a75b-21459232ce02"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "BLT@endlas.com", "Brett", "Trotter" });

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalSnapshot_JobId",
                table: "EnvironmentalSnapshot",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnvironmentalSnapshot_Jobs_JobId",
                table: "EnvironmentalSnapshot",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
