using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityCourseManagement.Migrations
{
    /// <inheritdoc />
    public partial class addIsDeletedColumnIntoClassRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClassRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClassRooms");
        }
    }
}
