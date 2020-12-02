using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileUpload.Data.Migrations
{
    public partial class SeparateThumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "ThumbnailId",
                table: "Files",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Thumbnails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Blob = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnails", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d2d8122-1111-4f47-9666-a7e5fde03bca",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b6cc092d-3fef-4e59-a63d-9a2a1be8a46d", "AQAAAAEAACcQAAAAEKwIJpnMdpE9qEXVCS86TbVxBBS4ueOad9HXriVMARSaq22UbL0cE0H0WHnNMFowjA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Files_ThumbnailId",
                table: "Files",
                column: "ThumbnailId",
                unique: true,
                filter: "[ThumbnailId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Thumbnails_ThumbnailId",
                table: "Files",
                column: "ThumbnailId",
                principalTable: "Thumbnails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Thumbnails_ThumbnailId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "Thumbnails");

            migrationBuilder.DropIndex(
                name: "IX_Files_ThumbnailId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Files");

            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "Files",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d2d8122-1111-4f47-9666-a7e5fde03bca",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66199cf0-56a4-4a68-b1e8-99df2be094cd", "AQAAAAEAACcQAAAAEAs0NyV03mzdsARBw+qDw916oqUj9KvVrMVxSomTRa6xF/OuzKP7Dys8Vl4rS45FlA==" });
        }
    }
}
