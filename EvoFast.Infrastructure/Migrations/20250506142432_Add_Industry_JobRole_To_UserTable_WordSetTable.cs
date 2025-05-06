using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Industry_JobRole_To_UserTable_WordSetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "WordSet",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobRole",
                table: "WordSet",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobRole",
                table: "User",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Industry",
                table: "WordSet");

            migrationBuilder.DropColumn(
                name: "JobRole",
                table: "WordSet");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "User");

            migrationBuilder.DropColumn(
                name: "JobRole",
                table: "User");
        }
    }
}
