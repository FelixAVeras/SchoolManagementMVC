using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagementMVC.Models;

namespace SchoolManagementMVC.Controllers
{
    public class StudentsController : Controller
    {
        private SchoolManagementMVCContext db = new SchoolManagementMVCContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Gender).Include(s => s.Positions).Include(s => s.State);
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
        public ActionResult Create(Student student)
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

            if (ModelState.IsValid)
            {
                if (student.ImageUrl != null)
                {
                    var pic = Path.GetFileName(student.ImageUrl.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images/Students/ProfilePic"), pic);
                    student.ImageUrl.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        student.ImageUrl.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }

                List<UploadFile> uploadFiles = new List<UploadFile>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        UploadFile uploadFile = new UploadFile()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        uploadFiles.Add(uploadFile);

                        var path = Path.Combine(Server.MapPath("~/Content/Images/Students/AttachFiles/"), uploadFile.Id + uploadFile.Extension);
                        file.SaveAs(path);
                    }
                }

                student.UploadFiles = uploadFiles;
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description", student.GenderID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", student.PositionID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "Description", student.StateID);
            return View(student);
        }

        // POST: Students/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(student).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description", student.GenderID);
            //ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", student.PositionID);
            //ViewBag.StateID = new SelectList(db.States, "StateID", "Description", student.StateID);
            //return View(student);

            if (ModelState.IsValid)
            {

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        UploadFile uploadFile = new UploadFile()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid(),
                            StudentID = student.StudentID
                        };
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Students/AttachFiles/"), uploadFile.Id + uploadFile.Extension);
                        file.SaveAs(path);

                        db.Entry(uploadFile).State = EntityState.Added;
                    }
                }

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
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
