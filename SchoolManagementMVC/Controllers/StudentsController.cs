using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SchoolManagementMVC.Models;

namespace SchoolManagementMVC.Controllers
{
    public class StudentsController : Controller
    {
        private SchoolManagementMVCContext db = new SchoolManagementMVCContext();

        // GET: Students
        // public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Gender).Include(s => s.Positions).Include(s => s.State);

           // ViewBag.CurrentSort = sortOrder;
           // ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           // ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

           // if (searchString != null)
           // {
           //     page = 1;
           // }
           // else
           // {
           //     searchString = currentFilter;
           // }
           // ViewBag.currentFilter = searchString;

           //students = from s in db.Students
           //                select s;

           // if(!String.IsNullOrEmpty(searchString))
           // {
           //     students = students.Where(
           //         s => s.LastName.Contains(searchString) ||
           //              s.FirstName.Contains(searchString)
           //     );

           // }

           // switch (sortOrder)
           // {
           //     case "name_desc":
           //         students = students.OrderByDescending(s => s.LastName);
           //         break;
           //     case "Date":
           //         students = students.OrderBy(s => s.FirstName);
           //         break;
           //     case "date_desc":
           //         students = students.OrderByDescending(s => s.FirstName);
           //         break;
           //     default:
           //         students = students.OrderBy(s => s.LastName);
           //         break;
           // }

            //int pageSize = 10;
            //int pageNumber = (page ?? 1);
            //return View(students.ToPagedList(pageNumber, pageSize));
            
            return View(students.ToList());

        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description");
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description");
            ViewBag.StateID = new SelectList(db.States, "StateID", "Description");
            return View();
        }

        // POST: Students/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentView studentView)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Students.Add(student);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description", student.GenderID);
            //ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", student.PositionID);
            //ViewBag.StateID = new SelectList(db.States, "StateID", "Description", student.StateID);
            //return View(student);

            if (!ModelState.IsValid)
            {
                ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description", studentView.GenderID);
                ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", studentView.PositionID);
                ViewBag.StateID = new SelectList(db.States, "StateID", "Description", studentView.StateID);
                return View(studentView);
            }

            string path = Server.MapPath("~/Content/Images/Students");
            string pic = string.Empty;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (studentView.ImageUrl != null)
            {
                pic = Path.GetFileName(studentView.ImageUrl.FileName);
                path = Path.Combine(Server.MapPath("~/Content/Images/Students"), pic);
                studentView.ImageUrl.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    studentView.ImageUrl.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            var student = new Student
            {
                Address = studentView.Address,
                BirthDate = studentView.BirthDate,
                Email = studentView.Email,
                EndTime = studentView.EndTime,
                EntryDate = studentView.EntryDate,
                FirstName = studentView.FirstName,
                ImageUrl = pic == string.Empty ? string.Empty : string.Format("~/Content/Images/Students/{0}", pic),
                LastName = studentView.LastName,
                Phone = studentView.Phone,
                PositionID = studentView.PositionID,
                Positions = studentView.Positions,
                Remarks = studentView.Remarks,
                StartTime = studentView.StartTime,
                StateID = studentView.StateID,
                State = studentView.State
            };

            db.Students.Add(student);

            try
            {
                ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description", studentView.GenderID);
                ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", studentView.PositionID);
                ViewBag.StateID = new SelectList(db.States, "StateID", "Description", studentView.StateID); 
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null)
                {
                    ViewBag.Error = ex.Message;
                }

                ViewBag.Error = ex.Message;

                return View(studentView);
            }

            return RedirectToAction("Index");
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            var studentView = new StudentView
            {
                StudentID = student.StudentID,
                Address = student.Address,
                BirthDate = student.BirthDate,
                Email = student.Email,
                EndTime = student.EndTime,
                EntryDate = student.EntryDate,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Phone = student.Phone,
                Positions = student.Positions,
                Remarks = student.Remarks,
                StartTime = student.StartTime,
                State = student.State
            };

            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description", student.GenderID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", student.PositionID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "Description", student.StateID);

            return View(studentView);
        }

        // POST: Students/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentView studentView)
        {
            if (!ModelState.IsValid)
            {
                return View(studentView);
                
            }

            string path = Server.MapPath("~/Content/Images/Students");
            string pic = string.Empty;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (studentView.ImageUrl != null)
            {
                pic = Path.GetFileName(studentView.ImageUrl.FileName);
                path = Path.Combine(Server.MapPath("~/Content/Images/Students"), pic);
                studentView.ImageUrl.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    studentView.ImageUrl.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            var student = db.Employees.Find(studentView.StudentID);

            student.Address = studentView.Address;
            student.BirthDate = studentView.BirthDate;
            student.Email = studentView.Email;
            student.EndTime = studentView.EndTime;
            student.EntryDate = studentView.EntryDate;
            student.FirstName = studentView.FirstName;
            student.LastName = studentView.LastName;
            student.Phone = studentView.Phone;
            student.PositionID = studentView.PositionID;
            student.Positions = studentView.Positions;
            student.Remarks = studentView.Remarks;
            student.StartTime = studentView.StartTime;
            student.StateID = studentView.StateID;
            student.State = studentView.State;

            if (!string.IsNullOrEmpty(pic))
            {
                student.ImageUrl = string.Format("~/Content/Images/Students/{0}", pic);
            }

            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description", student.GenderID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", student.PositionID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "Description", student.StateID);
            

            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
    }
}
