using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Attempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordSetAttempt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WordSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordSetAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordSetAttempt_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordSetAttempt_WordSet_WordSetId",
                        column: x => x.WordSetId,
                        principalTable: "WordSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAttempt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsBookmarked = table.Column<bool>(type: "boolean", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    WordSetAttemptId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAttempt_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAttempt_WordSetAttempt_WordSetAttemptId",
                        column: x => x.WordSetAttemptId,
                        principalTable: "WordSetAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempt_QuestionId",
                table: "QuestionAttempt",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempt_WordSetAttemptId_QuestionId",
                table: "QuestionAttempt",
                columns: new[] { "WordSetAttemptId", "QuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordSetAttempt_UserId_WordSetId",
                table: "WordSetAttempt",
                columns: new[] { "UserId", "WordSetId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordSetAttempt_WordSetId",
                table: "WordSetAttempt",
                column: "WordSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAttempt");

            migrationBuilder.DropTable(
                name: "WordSetAttempt");
        }
    }
}
