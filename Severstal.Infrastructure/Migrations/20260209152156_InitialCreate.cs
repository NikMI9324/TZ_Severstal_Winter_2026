using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Severstal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rolls",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    length = table.Column<double>(type: "double precision", nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    added_date = table.Column<DateTime>(type: "date", nullable: false),
                    removed_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolls", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rolls_added_date",
                table: "rolls",
                column: "added_date");

            migrationBuilder.CreateIndex(
                name: "IX_rolls_added_date_removed_date",
                table: "rolls",
                columns: new[] { "added_date", "removed_date" });

            migrationBuilder.CreateIndex(
                name: "IX_rolls_length",
                table: "rolls",
                column: "length");

            migrationBuilder.CreateIndex(
                name: "IX_rolls_removed_date",
                table: "rolls",
                column: "removed_date");

            migrationBuilder.CreateIndex(
                name: "IX_rolls_weight",
                table: "rolls",
                column: "weight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rolls");
        }
    }
}
