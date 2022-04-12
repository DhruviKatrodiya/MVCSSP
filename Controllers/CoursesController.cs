using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEntityFrameAppCS.Models;
using PagedList;

namespace MVCEntityFrameAppCS.Controllers
{
    public class CoursesController : Controller
    {
        private StudentDBEntities1 db = new StudentDBEntities1();

        // GET: Courses
        public ActionResult Index(string Sorting_Order , string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CourrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "CourseName" : "";

            if(Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;

            var course = from c in db.Courses select c;
            if(!String.IsNullOrEmpty(Search_Data))
            {
                course = course.Where(c => c.CourseName.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "CourseName":
                    course = course.OrderByDescending(c => c.CourseName);
                    break;

                default:
                    course = course.OrderBy(c => c.CourseName);
                    break;
            }
            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(course.ToPagedList(No_Of_Page,Size_Of_Page));

            //if a user choose the radio button option as Subject  
            //if (option == "CourseId")
            //{
            //    int value;
            //    if (search == null)
            //    {
            //        value = 0;
            //    }
            //    else
            //    {
            //        value = Convert.ToInt32(search);
            //    }
            //    var result = db.Courses.Where(c => c.CourseId.ToString().
            //    Contains(value.ToString()) || value == 0);
            //    return View(result.ToList());
            //}
            //else if (option == "CourseName")
            //{
            //    var result = db.Courses.Where(c => c.CourseName.Contains(search) || search == null);
            //    return View(result.ToList());
            //}


        }


        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult Search(string Search_Data)
        //{
            
        //    var course = from c in db.Courses select c;
        //    {
        //        course = course.Where(c => c.CourseName.ToUpper().Contains(Search_Data.ToUpper()));
        //    }
            
        //    return View(course.ToList());
        //}
    }
}
