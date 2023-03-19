using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                    { new Guid("2ea49d95-4e4e-44ee-950e-b273c0cbea8a"), null, null, "Charles Dickens", false },
                    { new Guid("3477e952-c34a-47c7-a3d9-39d136ce486f"), new DateTime(1564, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "William Shakespeare", false },
                    { new Guid("5a8085ee-1909-446c-8a24-3c3c45ccf9b6"), null, null, "Fyodor Dostoevsky", false },
                    { new Guid("86902c7c-c31a-4a4f-95e1-3eeca9c39e6e"), new DateTime(1848, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hristo Botev", false },
                    { new Guid("b65902a7-8c44-413a-99bc-86156211ae3b"), null, null, "Ernest Hemingway", false }
                });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "DateFounded", "DeletedAt", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("467d5837-8ff7-43ed-abf9-92fd430f4dc8"), new DateTime(1971, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Powell's Books" },
                    { new Guid("6c424a69-42b0-49db-b1a5-7c2e8820e22d"), new DateTime(1994, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Amazon" },
                    { new Guid("d53bfa82-def2-48ad-9cad-0ae619677155"), new DateTime(2008, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Ozone" }
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
