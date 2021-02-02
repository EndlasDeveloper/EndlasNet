using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EndlasNet.Data.Migrations
{
    public partial class migggg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthString", "Discriminator", "EndlasEmail", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("bc4daa13-4478-422e-9530-cff55d09da2d"), "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", "Admin", "SA@endlas.com", "Super", "Admin" },
                    { new Guid("18ef9a3e-7b4c-4a04-a37d-213ce09cbf62"), "10e4be5b8934f5279b7a10a0ed3988043561d2eccde97bc6ac9eb6062aa6221c", "Admin", "James.Tomich@endlas.com", "James", "Tomich" },
                    { new Guid("6662e17c-5730-42d9-8700-fafeda9f1ad4"), "4c2a671ebe8c3cd38f3e080470701b7bf2d2a4616d986475507c5153888b63f7", "Admin", "Josh.Hammell@endlas.com", "Josh", "Hammell" },
                    { new Guid("bb9bb09e-b1e8-4132-a10f-9a1985f9ca92"), "2209cf9aaea01490c254f7a0885fa6afc2ba6807cd27dcbc28e802f613e05c82", "Admin", "BLT@endlas.com", "Brett", "Trotter" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("18ef9a3e-7b4c-4a04-a37d-213ce09cbf62"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6662e17c-5730-42d9-8700-fafeda9f1ad4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bb9bb09e-b1e8-4132-a10f-9a1985f9ca92"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bc4daa13-4478-422e-9530-cff55d09da2d"));

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "Jobs");

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
        }
    }
}
