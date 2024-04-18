using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingSystem.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly StartPeriod { get; set; }
        public TimeOnly EndPeriod { get; set; }
        [ForeignKey("Track")]
        public int TrackID { get; set; }
        public virtual Track Track { get; set; }

        [ForeignKey("Supervisor")]
        public int InstructorID { get; set; }
        public virtual Instructor Supervisor { get; set; }

    }
}
