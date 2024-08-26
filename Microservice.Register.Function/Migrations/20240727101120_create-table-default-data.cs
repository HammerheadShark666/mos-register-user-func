using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Microservice.Register.Function.Migrations
{
    /// <inheritdoc />
    public partial class createtabledefaultdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MSOS_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Verified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MSOS_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MSOS_User",
                columns: new[] { "Id", "Created", "Email", "LastUpdated", "PasswordHash", "Role", "VerificationToken", "Verified" },
                values: new object[,]
                {
                    { new Guid("6c84d0a3-0c0c-435f-9ae0-4de09247ee15"), new DateTime(2024, 3, 27, 15, 31, 21, 70, DateTimeKind.Unspecified).AddTicks(3642), "intergration-test-user@example.com", new DateTime(2024, 7, 27, 11, 11, 18, 595, DateTimeKind.Local).AddTicks(5962), "$2a$11$K7TSYHDJaepUjxZPiE4dY.tuzpiL2JoEItsb3CVqwNkNELXIX2Ywy", 3, "-HpwWGVP5WXtxdvIH7VMNlJUyTS9_z9O7ef1BgPjhLcsjrOWxyyQNw44", new DateTime(2023, 8, 18, 15, 21, 38, 875, DateTimeKind.Unspecified).AddTicks(8226) },
                    { new Guid("929eaf82-e4fd-4efe-9cae-ce4d7e32d159"), new DateTime(2024, 3, 27, 15, 31, 45, 951, DateTimeKind.Unspecified).AddTicks(2476), "intergration-test-user2@example.com", new DateTime(2024, 7, 27, 11, 11, 18, 595, DateTimeKind.Local).AddTicks(6012), "$2a$11$1hPEhBElDwFfKDstC5j7EeGebkAKHyKEdVguvu2GOREdm8qNpbNOi", 3, "-AbTPQWp3vTaExY6q3SF9nAqsVAulzTmTkuj-gfmv_5-XDabkYa1EQ44", new DateTime(2023, 8, 18, 15, 26, 46, 293, DateTimeKind.Unspecified).AddTicks(65) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MSOS_User");
        }
    }
}
