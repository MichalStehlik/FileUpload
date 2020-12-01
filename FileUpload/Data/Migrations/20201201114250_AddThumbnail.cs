using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileUpload.Data.Migrations
{
    public partial class AddThumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "Files",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d2d8122-1111-4f47-9666-a7e5fde03bca",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66199cf0-56a4-4a68-b1e8-99df2be094cd", "AQAAAAEAACcQAAAAEAs0NyV03mzdsARBw+qDw916oqUj9KvVrMVxSomTRa6xF/OuzKP7Dys8Vl4rS45FlA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Files");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d2d8122-1111-4f47-9666-a7e5fde03bca",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f188b659-3e07-405b-a2de-4a362f9d38aa", "AQAAAAEAACcQAAAAEF30hyCbwTvEaSXjKmfgXHb0OyUCU89rAC+4tiWe1Hi/nT5GIywWKSuPyhHIzb3ZSQ==" });
        }
    }
}
