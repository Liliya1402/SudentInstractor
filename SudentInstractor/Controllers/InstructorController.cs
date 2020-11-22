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

            ViewBag.Course = _dbc.Courses.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Instructor instructor, int courseId)
        {
            if (ModelState.IsValid)
            {
                instructor.Id = _dbc.Instructors.Last().Id + 1;
                instructor.Course = _dbc.Courses.Find(courseId);

                _dbc.Instructors.Add(instructor);
                _dbc.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(instructor);
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
               .Include(i=>i.Course)
               .FirstOrDefault();
            ViewBag.Courses = _dbc.Courses.ToList();
           

            ViewBag.CourseId = instructor.Course.Id;

            _dbc.SaveChanges();
            return View(instructor);

        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Instructor inst)
        {


            try
            {
                if (ModelState.IsValid)
                {

                    var instructor = _dbc.Instructors.Where(i => i.Id == inst.Id).FirstOrDefault();
                    if (inst != null)
                    {
                        instructor.Id = inst.Id;
                        instructor.FirstName = inst.FirstName;
                        instructor.LastName = inst.LastName;
                        instructor.Email = inst.Email;

                        _dbc.Instructors.Remove(instructor);
                        _dbc.Instructors.Add(inst);
                        _dbc.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (DataMisalignedException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again");
            }
            return View(inst);
            }

            [HttpGet]
        public IActionResult Delete(int? id)
        {
            Instructor instr = _dbc.Instructors.Where(i => i.Id == id).FirstOrDefault();

            return View(instr);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int ?id)
        {
           
            
                Instructor instr = _dbc.Instructors.Where(i => i.Id == id).FirstOrDefault();
                _dbc.Instructors.Remove(instr);
              
                _dbc.SaveChanges();
           
            return RedirectToAction("Index");
        }
    }

   }




