using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SudentInstractor.Data;
using SudentInstractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudentInstractor.Controllers
{
    public class InstructorController : Controller
    {
        private readonly CollegeDbCotext _dbc = new CollegeDbCotext();
        public IActionResult Index()
        {

            List<Instructor> instructors = _dbc
                .Instructors
                .Include("Course")
                .ToList();

            return View(instructors);
        }


        [HttpGet]
        public IActionResult Create()
        {

            ViewBag.Courses = _dbc.Courses.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Instructor instructor,int courseId)
        {
       
                instructor.Id = _dbc.Instructors.Last().Id + 1;
                instructor.Course = _dbc.Courses.Find(courseId);

                _dbc.Instructors.Add(instructor);
                _dbc.SaveChanges();

                return RedirectToAction("Index");
            }
     
        public IActionResult Details(int? id)
        {
            Instructor instructor = _dbc.Instructors.Where(i => i.Id == id).FirstOrDefault();
           
            List<Course> courses = _dbc.StudentCourses
                .Where(sc => sc.StudentId == id)
                .Select(c => c.Course)
                .ToList();

            ViewBag.Courses = courses;
            return View(instructor);
        }



        public IActionResult Edit(int id)
        {
            Instructor instructor = _dbc.Instructors
               .Where(i => i.Id == id)
               .FirstOrDefault();
            ViewBag.Courses = _dbc.Courses.ToList();
           

            ViewBag.CourseId = instructor.Course.Id;

            _dbc.SaveChanges();
            return View(instructor);

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Instructor instr = _dbc.Instructors.Where(i => i.Id == id).FirstOrDefault();

            return View(instr);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
           
            
                Instructor instr = _dbc.Instructors.Where(i => i.Id == id).FirstOrDefault();
                _dbc.Instructors.Remove(instr);
                _dbc.SaveChanges();
           
            return RedirectToAction("Index");
        }
    }

   }




