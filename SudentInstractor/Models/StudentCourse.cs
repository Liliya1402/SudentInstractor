using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SudentInstractor.Models
{
    [Table("StudentCourse")]
    public class StudentCourse
    {
        public int StudentId { set; get; }
        public int CourseId { set; get; }

        public Student Student { set; get; }
        public Course Course { set; get; }
        
    }
}
