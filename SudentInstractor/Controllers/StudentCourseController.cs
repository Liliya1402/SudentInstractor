using Microsoft.AspNetCore.Mvc;
using SudentInstractor.Data;
using SudentInstractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudentInstractor.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly CollegeDbCotext _dbc = new CollegeDbCotext();


        public IActionResult AddCourse(int studentId, int courseId)
        {
            

            _dbc.StudentCourses
                .Add(new StudentCourse
                {
                    StudentId= studentId,
                    CourseId = courseId
                });

            _dbc.SaveChanges();

            return RedirectToAction("Edit", "Student", new { id = studentId });

        }
      
        }


    }

