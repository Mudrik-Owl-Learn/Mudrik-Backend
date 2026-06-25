using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mudrik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuizQuestionManyAndChunkNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AgentGeneratedQuizzes_StudentProfileId_LessonMicroChunkId_AttemptNumber",
                table: "AgentGeneratedQuizzes");

            migrationBuilder.DropColumn(
            name: "ReferenceId",
            table: "XpTransactions");

            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceId",
                table: "XpTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonMicroChunkId",
                table: "AgentGeneratedQuizzes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "AgentGeneratedQuizQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentGeneratedQuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentGeneratedQuizQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentGeneratedQuizQuestion_AgentGeneratedQuizzes_AgentGeneratedQuizId",
                        column: x => x.AgentGeneratedQuizId,
                        principalTable: "AgentGeneratedQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentGeneratedQuizQuestion_QuizQuestions_QuizQuestionId",
                        column: x => x.QuizQuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentGeneratedQuizzes_StudentProfileId_LessonMicroChunkId_AttemptNumber",
                table: "AgentGeneratedQuizzes",
                columns: new[] { "StudentProfileId", "LessonMicroChunkId", "AttemptNumber" },
                unique: true,
                filter: "[LessonMicroChunkId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AgentGeneratedQuizzes_StudentProfileId_StandardLessonId_AttemptNumber",
                table: "AgentGeneratedQuizzes",
                columns: new[] { "StudentProfileId", "StandardLessonId", "AttemptNumber" },
                unique: true,
                filter: "[LessonMicroChunkId] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AgentGeneratedQuizQuestion_AgentGeneratedQuizId",
                table: "AgentGeneratedQuizQuestion",
                column: "AgentGeneratedQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentGeneratedQuizQuestion_QuizQuestionId",
                table: "AgentGeneratedQuizQuestion",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
    name: "IX_AgentGeneratedQuizQuestion_AgentGeneratedQuizId_QuizQuestionId",
    table: "AgentGeneratedQuizQuestion",
    columns: new[] { "AgentGeneratedQuizId", "QuizQuestionId" },
    unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentGeneratedQuizQuestion");

            migrationBuilder.DropIndex(
                name: "IX_AgentGeneratedQuizzes_StudentProfileId_LessonMicroChunkId_AttemptNumber",
                table: "AgentGeneratedQuizzes");

            migrationBuilder.DropIndex(
                name: "IX_AgentGeneratedQuizzes_StudentProfileId_StandardLessonId_AttemptNumber",
                table: "AgentGeneratedQuizzes");


            migrationBuilder.DropColumn(
            name: "ReferenceId",
            table: "XpTransactions");

            migrationBuilder.AddColumn<int>(
                name: "ReferenceId",
                table: "XpTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonMicroChunkId",
                table: "AgentGeneratedQuizzes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgentGeneratedQuizzes_StudentProfileId_LessonMicroChunkId_AttemptNumber",
                table: "AgentGeneratedQuizzes",
                columns: new[] { "StudentProfileId", "LessonMicroChunkId", "AttemptNumber" },
                unique: true);
        }
    }
}
