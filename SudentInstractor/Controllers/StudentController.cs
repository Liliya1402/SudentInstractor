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
            if (student == null)
            {
                return HttpNotFound();
            }
            List<Course> courses = _dbc.StudentCourses
                .Where(sc => sc.StudentId == id)
                .Select(c => c.Course)
                .ToList();
             ViewBag.Courses = courses;
            return View(student);
        }

        private IActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Courses = _dbc.Courses.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            
            Student std = _dbc.Students.Where(s => s.StudentID == id).FirstOrDefault();

            List<Course> courses = _dbc.StudentCourses
                .Where(sc => sc.StudentId == id)
                .Select(c => c.Course)
                .ToList();

            ViewBag.Courses = courses;

            ViewBag.AvailableCourses = _dbc.Courses
                .Except(courses)
                .ToList();

            return View(std);
        }
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student std)
        {
          

            try
            {
                if (ModelState.IsValid)
                {

                    var student = _dbc.Students.Where(s => s.StudentID == std.StudentID).FirstOrDefault();
                    if (std != null)
                    {
                        student.StudentID = std.StudentID;
                        student.FirstName = std.FirstName;
                        student.LastName = std.LastName;
                        student.Email = std.Email;
                        student.Phone = std.Phone;

                        _dbc.Students.Remove(student);
                        _dbc.Students.Add(std);
                        _dbc.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch(DataMisalignedException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again");
            }

            return View(std);
        }



        [HttpGet]
        public IActionResult Delete(int? id, bool? saveChangesError = false)

        {
            if (id == null)
            {
                return NotFound();
            }


            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again.";
            }

            Student st = _dbc.Students.Where(s => s.StudentID == id).FirstOrDefault();

            if (st == null)
            {
                return HttpNotFound();
            }

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

        protected override void Dispose(bool disposing)
        {
            _dbc.Dispose();
            base.Dispose(disposing);
        }
    }
}