using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class updateSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateOnly>(type: "date", nullable: false),
                    StartPeriod = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndPeriod = table.Column<TimeOnly>(type: "time", nullable: false),
                    TrackID = table.Column<int>(type: "int", nullable: false),
                    InstructorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedules", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_schedules_instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedules_tracks_TrackID",
                        column: x => x.TrackID,
                        principalTable: "tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            
            migrationBuilder.CreateIndex(
                name: "IX_schedules_TrackID",
                table: "schedules",
                column: "TrackID");

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropTable(
                name: "schedules");

         
        }
    }
}
