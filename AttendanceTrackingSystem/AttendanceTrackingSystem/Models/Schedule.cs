using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingSystem.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int Day { get; set; }
        public int StartPeriod { get; set; }
        public int EndPeriod { get; set; }
        [ForeignKey("Track")]
        public int TrackID { get; set; }
        public virtual Track Track { get; set; }

        [ForeignKey("Supervisor")]
        public int InstructorID { get; set; }
        public virtual Instructor Supervisor { get; set; }

    }
}
