using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SudentInstractor.Models
{
    public class Course
    {
       
        public int Id { get; set; }

        [Display(Name ="Course Number")]
        [Required(ErrorMessage = "Enter course number")]
        public string CourseNo { get; set; }

       
        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "Enter Course Name")]
        public string Name { get; set; }
        public string Description { get; set; }
         List<Instructor> Instructors {  set; get; }
         List<StudentCourse> StudentCourses { set; get; }
    }
}
