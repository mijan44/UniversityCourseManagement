using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityCourseManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResultEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Courses_CourseId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_RegisterStudents_RegisterStudentId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_CourseId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_RegisterStudentId",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "RegisterStudentId",
                table: "Results",
                newName: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Results",
                newName: "RegisterStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_CourseId",
                table: "Results",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_RegisterStudentId",
                table: "Results",
                column: "RegisterStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Courses_CourseId",
                table: "Results",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_RegisterStudents_RegisterStudentId",
                table: "Results",
                column: "RegisterStudentId",
                principalTable: "RegisterStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
