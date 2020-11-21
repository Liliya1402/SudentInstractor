using Microsoft.AspNetCore.Mvc;
using SudentInstractor.Data;
using SudentInstractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpPost]
        public IActionResult Edit(Course course)
        {
            Course cr = _dbCollege.Courses.Where(c => c.Id == course.Id).FirstOrDefault();

            if (cr != null)
            {
                cr.Id = course.Id;
                cr.Name = course.Name;
                cr.CourseNo = course.CourseNo;
                cr.Description = course.Description;



                _dbCollege.SaveChanges();
            }

            return RedirectToAction("Index");
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

        public IActionResult Delete(int? id)
        {
            Course cur = _dbCollege.Courses.Where(c => c.Id == id).FirstOrDefault();

            return ViewBag(cur);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Course cur = _dbCollege.Courses.Where(c => c.Id == id).FirstOrDefault();
            _dbCollege.Remove(cur);
            _dbCollege.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}





