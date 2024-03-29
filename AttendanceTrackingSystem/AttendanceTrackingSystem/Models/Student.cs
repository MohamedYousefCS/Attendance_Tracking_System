namespace AttendanceTrackingSystem.Models
{
    public class Student:User
    {


        public string University { get; set; }
        public string Faculty { get; set; }
        public string Specialization { get; set; }
        public int GraduationYear { get; set; }
        public string StudentTrack { get; set; }



    }
}
