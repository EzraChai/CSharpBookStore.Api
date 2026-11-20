using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Api.Migrations;

/// <inheritdoc />
public partial class _20261120041011_InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                ISBN = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                Title = table.Column<string>(type: "TEXT", nullable: false),
                Author = table.Column<string>(type: "TEXT", nullable: false),
                Price = table.Column<decimal>(type: "TEXT", nullable: false),
                PublishedDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Books");
    }
}
