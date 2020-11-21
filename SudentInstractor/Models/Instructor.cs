using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SudentInstractor.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Enter your first  Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Enter your Last  Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Provide your email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
     
        [ForeignKey("CourseId")]
        public Course Course { set; get; }
     

    }
}
