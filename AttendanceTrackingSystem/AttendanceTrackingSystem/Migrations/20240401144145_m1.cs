using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "StudentTrack",
                table: "students",
                newName: "BirthDate");

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "students",
                newName: "StudentTrack");

            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
