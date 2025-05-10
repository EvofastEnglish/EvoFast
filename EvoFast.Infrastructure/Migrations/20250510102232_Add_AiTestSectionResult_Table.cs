using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_AiTestSectionResult_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AiTestSectionResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestSectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Evaluation = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiTestSectionResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiTestSectionResult_AiTestResults_AiTestResultId",
                        column: x => x.AiTestResultId,
                        principalTable: "AiTestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AiTestSectionResult_AiTestSections_AiTestSectionId",
                        column: x => x.AiTestSectionId,
                        principalTable: "AiTestSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AiTestSectionResult_AiTestResultId",
                table: "AiTestSectionResult",
                column: "AiTestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_AiTestSectionResult_AiTestSectionId_AiTestResultId",
                table: "AiTestSectionResult",
                columns: new[] { "AiTestSectionId", "AiTestResultId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiTestSectionResult");
        }
    }
}
