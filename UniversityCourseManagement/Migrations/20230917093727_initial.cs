using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityCourseManagement.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterStudents_Departments_DepartmentId",
                table: "RegisterStudents");

            migrationBuilder.DropIndex(
                name: "IX_RegisterStudents_DepartmentId",
                table: "RegisterStudents");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "RegisterStudents");

            migrationBuilder.AlterColumn<int>(
                name: "RegistrationNumber",
                table: "RegisterStudents",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "RegisterStudents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "RegisterStudents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RegisterStudents_DepartmentId",
                table: "RegisterStudents",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterStudents_Departments_DepartmentId",
                table: "RegisterStudents",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
