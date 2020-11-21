using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SudentInstractor.Data;
using SudentInstractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudentInstractor.Controllers
{
    public class StudentController : Controller
    {
        private readonly CollegeDbCotext _dbc = new CollegeDbCotext();


        public IActionResult Index()
        {
            List<Student> students = _dbc.Students.ToList();

            return View(students);
        }

        public IActionResult Details(int id)
        {
            Student student = _dbc.Students.Where(s => s.StudentID == id).FirstOrDefault();
           
            List<Course> courses = _dbc.StudentCourses
                .Where(sc => sc.StudentId == id)
                .Select(c => c.Course)
                .ToList();
             ViewBag.Courses = courses;
            return View(student);
        }
        

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {

            if (ModelState.IsValid)
            {
                student.StudentID = _dbc.Students.Last().StudentID + 1;
                _dbc.Students.Add(student);
                _dbc.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(student);
        }

        public IActionResult Edit(int id)
        {
            Student student = _dbc.Students.Where(s => s.StudentID == id).FirstOrDefault();

            List<Course> courses = _dbc.StudentCourses
                .Where(sc => sc.StudentId == id)
                .Select(c => c.Course)
                .ToList();

            ViewBag.Courses = courses;

            ViewBag.AvailableCourses = _dbc.Courses
                .Except(courses)
                .ToList();

            return View(student);
        }



        [HttpGet]
        public IActionResult Delete(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            Student st = _dbc.Students.Where(s => s.StudentID == id).FirstOrDefault();

            return View(st);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Student st = _dbc.Students.Where(s => s.StudentID == id).FirstOrDefault();
            _dbc.Students.Find(id);
            _dbc.Students.Remove(st);
            _dbc.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}