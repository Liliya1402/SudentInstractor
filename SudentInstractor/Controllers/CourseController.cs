using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using SudentInstractor.Data;
using SudentInstractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace SudentInstractor.Controllers
{
    public class CourseController : Controller
    {

        private CollegeDbCotext _dbCollege = new CollegeDbCotext();
        public IActionResult Index()

        {

            return View(_dbCollege.Courses.ToList());
        }
        public IActionResult Details(int id)
        {

            return View(_dbCollege.Courses.Where(c => c.Id == id).ToList());

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            return View(_dbCollege.Courses.Where(c => c.Id == id).FirstOrDefault());
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {


            if (ModelState.IsValid)
            {
                Course cr = _dbCollege.Courses.Where(c => c.Id == course.Id).FirstOrDefault();

                if (cr != null)
                {
                    cr.Id = course.Id;
                    cr.Name = course.Name;
                    cr.CourseNo = course.CourseNo;
                    cr.Description = course.Description;
                    _dbCollege.Courses.Remove(cr);
                    _dbCollege.Courses.Add(course);
                    _dbCollege.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(course);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {


            course.Id = _dbCollege.Courses.Last().Id + 1;

            _dbCollege.Courses.Add(course);
            _dbCollege.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id, bool? saveChangesError = false)
        {
           

            Course course = _dbCollege.Courses.Where(c => c.Id == id).FirstOrDefault();

            return View(course);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
           
           
                     Course cur = _dbCollege.Courses.Where(c => c.Id == id).FirstOrDefault();
                _dbCollege.Remove(cur);
                _dbCollege.SaveChanges();
         

          //  return RedirectToPage("Index");

             return RedirectToAction("Delete", new { Id = id, saveChangesError = true });
        }

            
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbCollege.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}





