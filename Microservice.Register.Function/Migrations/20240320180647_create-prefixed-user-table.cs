using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Microservice.Register.Function.Migrations
{
    /// <inheritdoc />
    public partial class createprefixedusertable : Migration
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
                    { new Guid("2385de72-2302-4ced-866a-fa199116ca6e"), new DateTime(2023, 8, 18, 15, 40, 30, 801, DateTimeKind.Unspecified).AddTicks(3615), "corday13@hotmail.com", new DateTime(2024, 3, 20, 18, 6, 46, 238, DateTimeKind.Local).AddTicks(654), "$2a$11$/k9/nqKpHgXlr6CTMVoZbu8HBLCebf1NdrP71LcgQWrVmFl/FP7.S", 3, "NYCscmP1Q3z76pQY_Y01Cdqwe_khc_YpwxkLwqGukgrMVDOEBitxQg44", new DateTime(2023, 8, 18, 15, 40, 30, 801, DateTimeKind.Unspecified).AddTicks(4311) },
                    { new Guid("47417642-87d9-4047-ae13-4c721d99ab48"), new DateTime(2023, 8, 18, 15, 42, 7, 633, DateTimeKind.Unspecified).AddTicks(1653), "corday14@hotmail.com", new DateTime(2024, 3, 20, 18, 6, 46, 238, DateTimeKind.Local).AddTicks(666), "$2a$11$wdkVIkHA1Cr0EZeE3Jk1seCSl5OT/hDlXjR6LpSY3XItN7F/gQp4S", 3, "d4fWNZNRF98DltZ3SKo3ggp2N-P3lG-1LOUiyv2mfPprdCJFxtZXdQ44", new DateTime(2023, 8, 18, 15, 42, 7, 633, DateTimeKind.Unspecified).AddTicks(2373) },
                    { new Guid("55b431ff-693e-4664-8f65-cfd8d0b14b1b"), new DateTime(2023, 8, 18, 15, 30, 59, 231, DateTimeKind.Unspecified).AddTicks(9879), "corday12@hotmail.com", new DateTime(2024, 3, 20, 18, 6, 46, 238, DateTimeKind.Local).AddTicks(642), "$2a$11$R26bWvkESNmNSeLOcpNDOebwu2vUQP6RaOCB18ujskdd/Gctpzy3m", 3, "JikIR425mjxRk2vRKHdYgYfscjsMx-nPqs8xzk5V99420MxFFXQDdg44", new DateTime(2023, 8, 18, 15, 30, 59, 232, DateTimeKind.Unspecified).AddTicks(1080) },
                    { new Guid("5ff79dfe-c1fa-4dd9-996f-bc96649d6dfc"), new DateTime(2023, 8, 18, 15, 48, 31, 270, DateTimeKind.Unspecified).AddTicks(6793), "corday16@hotmail.com", new DateTime(2024, 3, 20, 18, 6, 46, 238, DateTimeKind.Local).AddTicks(692), "$2a$11$op7y2GsDxvl7cZqqRrBpRuNf85luc.bVdWqRGSkh9XWbPnKNO9dXm", 3, "l-K_Il3aA25ZDAETMzhfqXMflCE0C_8vfG20pNd0isnTomQ1YN2fpw44", new DateTime(2023, 8, 18, 15, 48, 31, 270, DateTimeKind.Unspecified).AddTicks(7367) },
                    { new Guid("aa1dc96f-3be5-41cd-8a1b-207284af3fdd"), new DateTime(2023, 8, 18, 15, 21, 38, 875, DateTimeKind.Unspecified).AddTicks(7677), "corday10@hotmail.com", new DateTime(2024, 3, 20, 18, 6, 46, 238, DateTimeKind.Local).AddTicks(606), "$2a$11$sUclSCnqTUlmXReP68uXxuqlf1TW0RjG9lovnv21/Z6R4BYoH7XeC", 3, "lGoeUM53kxNLZlkznM4rpDjWTJa9rNcyjCAKonHeeX08TtM0C8wpbQ44", new DateTime(2023, 8, 18, 15, 21, 38, 875, DateTimeKind.Unspecified).AddTicks(8226) },
                    { new Guid("ae55b0d1-ba02-41e1-9efa-9b4d4ac15eec"), new DateTime(2023, 8, 18, 15, 53, 1, 715, DateTimeKind.Unspecified).AddTicks(4228), "corday17@hotmail.com", new DateTime(2024, 3, 20, 18, 6, 46, 238, DateTimeKind.Local).AddTicks(704), "$2a$11$kbeO4QR4kdl/uxLRSmGdJOSEs2PRNQFFPKFxthqICX0maXxEfNZ5C", 3, "mEId4vbifX3S5LfPd0eEhQ7BgNs6xz6cDwLPTBuPuwc6OFf8Yw2hBw44", new DateTime(2023, 8, 18, 15, 53, 1, 715, DateTimeKind.Unspecified).AddTicks(4754) },
                    { new Guid("af95fb7e-8d97-4892-8da3-5e6e51c54044"), new DateTime(2023, 8, 18, 15, 26, 46, 292, DateTimeKind.Unspecified).AddTicks(9450), "corday11@hotmail.com", new DateTime(2024, 3, 20, 18, 6, 46, 238, DateTimeKind.Local).AddTicks(630), "$2a$11$NWAxlt6tssER3IVzNA7Xw.b.ZCim3T3z58JDz/ilPmz/JhHutjDkS", 3, "5rGMUNyYN85wyB9o7Hn2ylEIo8w3Tlt3LtB8cSm4ks6wfF0TiMz9eQ44", new DateTime(2023, 8, 18, 15, 26, 46, 293, DateTimeKind.Unspecified).AddTicks(65) },
                    { new Guid("c95ba8ff-06a1-49d0-bc45-83f89b3ce820"), new DateTime(2023, 8, 18, 15, 55, 44, 946, DateTimeKind.Unspecified).AddTicks(2678), "corday18@hotmail.com", new DateTime(2024, 3, 20, 18, 6, 46, 238, DateTimeKind.Local).AddTicks(715), "$2a$11$jBIzZuggUZu52NOWAjlv6e2rVpIARa87JqSxdKUOe1RZ2KVZdSmDa", 3, "B8eXsXrt4NqdYwjiJWpVYVUGPdnetlF-Gt8pbBJvqjp8hH6CsentXQ44", new DateTime(2023, 8, 18, 15, 55, 44, 946, DateTimeKind.Unspecified).AddTicks(3214) },
                    { new Guid("f07e88ac-53b2-4def-af07-957cbb18523c"), new DateTime(2023, 8, 18, 16, 3, 37, 86, DateTimeKind.Unspecified).AddTicks(5647), "corday19@hotmail.com", new DateTime(2024, 3, 20, 18, 6, 46, 238, DateTimeKind.Local).AddTicks(728), "$2a$11$7Gb7oXNl0fMzRK3S6.2s1OUJ5tPTvlG44MgyIiguYZCsOYFCS98de", 3, "FwqSTqfEtdJanlaWArkYn5M4YH9lAHWBVq3HHtqK-Htw5pUlPy7R4w44", new DateTime(2023, 8, 18, 16, 3, 37, 86, DateTimeKind.Unspecified).AddTicks(6301) },
                    { new Guid("ff4d5a80-81e3-42e3-8052-92cf5c51e797"), new DateTime(2023, 8, 18, 15, 45, 55, 760, DateTimeKind.Unspecified).AddTicks(1533), "corday15@hotmail.com", new DateTime(2024, 3, 20, 18, 6, 46, 238, DateTimeKind.Local).AddTicks(681), "$2a$11$H8BS6j19gUmelCX7F1pUeul0.27ARe7H9Ux3O9s.LBOfXyPgKGLDi", 3, "TC9IVqrD-n6nWkfJH0i4OT8rHM7g8xo08WrnWfAehrRy8xSgG_zgRQ44", new DateTime(2023, 8, 18, 15, 45, 55, 760, DateTimeKind.Unspecified).AddTicks(2347) }
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
