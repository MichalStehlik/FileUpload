using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileUpload.Data.Migrations
{
    public partial class AddStoredFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UploaderId = table.Column<string>(nullable: false),
                    Uploaded = table.Column<DateTime>(nullable: false),
                    OriginalName = table.Column<string>(nullable: false),
                    ContentType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_AspNetUsers_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3d2d8122-1111-4f47-9666-a7e5fde03bca", 0, "f188b659-3e07-405b-a2de-4a362f9d38aa", "st@pslib.cloud", true, false, null, "ST@PSLIB.CLOUD", "ST@PSLIB.CLOUD", "AQAAAAEAACcQAAAAEF30hyCbwTvEaSXjKmfgXHb0OyUCU89rAC+4tiWe1Hi/nT5GIywWKSuPyhHIzb3ZSQ==", null, false, "", false, "st@pslib.cloud" });

            migrationBuilder.CreateIndex(
                name: "IX_Files_UploaderId",
                table: "Files",
                column: "UploaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d2d8122-1111-4f47-9666-a7e5fde03bca");
        }
    }
}
