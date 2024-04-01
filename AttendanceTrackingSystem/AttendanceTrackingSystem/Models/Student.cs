﻿using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Models
{
    public class Student:User
    {

        public Role role { get;private set; }=Role.Student;

        [Required(ErrorMessage = "University is required.")]
        [MinLength(10, ErrorMessage = "University must be at least 10 characters.")]
        [MaxLength(20, ErrorMessage = "University cannot exceed 20 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Invalid Name")]

        public string University { get; set; }

        [Required(ErrorMessage = "Faculty is Required. ")]
        [MinLength(5, ErrorMessage = "Faculty must be at least 5 characters.")]
        [MaxLength(20, ErrorMessage = " Faculty cannot exceed 20 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Invalid Name")]

        public string Faculty { get; set; }

        [Required(ErrorMessage = "Specialization is Required. ")]
        [MinLength(2, ErrorMessage = "Specialization must be at least 2 characters.")]
        [MaxLength(20, ErrorMessage = " Specialization cannot exceed 20 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Invalid Name")]

        public string Specialization { get; set; }

        [Required(ErrorMessage = "GraduationYear is Required. ")]
        [Range(2014, int.MaxValue, ErrorMessage = "Graduation year must be between 2015 and the current year.")]
        public int GraduationYear { get; set; }


    }
}
