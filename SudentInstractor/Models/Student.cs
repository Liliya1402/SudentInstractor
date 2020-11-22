using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SudentInstractor.Models
{
    public class Student
    {

        [Display(Name = "Student Id")]
        [Required(ErrorMessage = "Required Id")]
        public int StudentID { get; set; }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Enter your First  Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Enter your Last  Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Provide your email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Provide your phone number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        
        public List<StudentCourse> StudentCourses { set; get; }
    }


}
