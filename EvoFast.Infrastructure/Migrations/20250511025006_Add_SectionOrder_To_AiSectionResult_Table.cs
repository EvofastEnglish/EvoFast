using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_SectionOrder_To_AiSectionResult_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionOrder",
                table: "AiTestSectionResult",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectionOrder",
                table: "AiTestSectionResult");
        }
    }
}
