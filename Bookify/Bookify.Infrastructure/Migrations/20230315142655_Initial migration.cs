using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateFounded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PagesCount = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookMerchant",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookMerchant", x => new { x.BooksId, x.MerchantsId });
                    table.ForeignKey(
                        name: "FK_BookMerchant_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookMerchant_Merchants_MerchantsId",
                        column: x => x.MerchantsId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "DeletedAt", "FullName", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("199ba95a-78a2-4daf-99a6-dea956165284"), null, null, "Fyodor Dostoevsky", false },
                    { new Guid("b81c7fb8-0e22-44a3-b3c9-bfb74866871a"), new DateTime(1564, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "William Shakespeare", false },
                    { new Guid("b948bddf-e10c-4419-aab3-e5987007bbbf"), null, null, "Charles Dickens", false },
                    { new Guid("bc92a660-649b-4c37-88c2-f0458d261013"), null, null, "Ernest Hemingway", false },
                    { new Guid("cb6d2d87-dcd5-4cb5-be2b-483482c0d76e"), new DateTime(1848, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hristo Botev", false }
                });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "DateFounded", "DeletedAt", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("76058411-1087-4bdf-90cc-b78921052b6d"), new DateTime(1971, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Powell's Books" },
                    { new Guid("b3175c19-39e1-4936-bb84-ad85abbeece4"), new DateTime(1994, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Amazon" },
                    { new Guid("b5b03ff8-b9d7-42f1-94a3-c35bfd94126d"), new DateTime(2008, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Ozone" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookMerchant_MerchantsId",
                table: "BookMerchant",
                column: "MerchantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookMerchant");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
