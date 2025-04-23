using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_CompletedAt_To_WordSetAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "WordSetAttempt",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "WordSetAttempt");
        }
    }
}
