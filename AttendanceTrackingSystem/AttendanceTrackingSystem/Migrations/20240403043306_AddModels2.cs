using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddModels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permissionRequests_admins_AdminId",
                table: "permissionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_permissionRequests_employees_EmployeeId",
                table: "permissionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_permissionRequests_instructors_InstructorId",
                table: "permissionRequests");

            migrationBuilder.DropIndex(
                name: "IX_permissionRequests_AdminId",
                table: "permissionRequests");

            migrationBuilder.DropIndex(
                name: "IX_permissionRequests_EmployeeId",
                table: "permissionRequests");

            migrationBuilder.DropIndex(
                name: "IX_permissionRequests_InstructorId",
                table: "permissionRequests");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "permissionRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "permissionRequests");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "permissionRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "permissionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "permissionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "permissionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissionRequests_AdminId",
                table: "permissionRequests",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_permissionRequests_EmployeeId",
                table: "permissionRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_permissionRequests_InstructorId",
                table: "permissionRequests",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_permissionRequests_admins_AdminId",
                table: "permissionRequests",
                column: "AdminId",
                principalTable: "admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_permissionRequests_employees_EmployeeId",
                table: "permissionRequests",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_permissionRequests_instructors_InstructorId",
                table: "permissionRequests",
                column: "InstructorId",
                principalTable: "instructors",
                principalColumn: "Id");
        }
    }
}
