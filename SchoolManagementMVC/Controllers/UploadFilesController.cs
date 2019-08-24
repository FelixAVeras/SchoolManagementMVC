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
    public class UploadFilesController : Controller
    {
        private SchoolManagementMVCContext db = new SchoolManagementMVCContext();

        // GET: UploadFiles
        public ActionResult Index()
        {
            var uploadFiles = db.UploadFiles.Include(u => u.Employees).Include(u => u.Position).Include(u => u.Students);
            return View(uploadFiles.ToList());
        }

        // GET: UploadFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadFile uploadFile = db.UploadFiles.Find(id);
            if (uploadFile == null)
            {
                return HttpNotFound();
            }
            return View(uploadFile);
        }

        // GET: UploadFiles/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FullName");
            return View();
        }

        // POST: UploadFiles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UploadFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                List<FileDetail> fileDetails = new List<FileDetail>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        fileDetails.Add(fileDetail);

                        var path = Path.Combine(Server.MapPath("~/Content/Images/AttachFiles/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }

                uploadFile.FileDetails = fileDetails;
                db.UploadFiles.Add(uploadFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", uploadFile.PositionID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName", uploadFile.EmployeeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", uploadFile.StudentID);
            return View(uploadFile);
        }

        // GET: UploadFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadFile uploadFile = db.UploadFiles.Find(id);
            if (uploadFile == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", uploadFile.EmployeeID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", uploadFile.PositionID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FullName", uploadFile.StudentID);
            return View(uploadFile);
        }

        // POST: UploadFiles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UploadFile uploadFile)
        {
            if (ModelState.IsValid)
            {

                //New Files
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid(),
                            UploadFileID = uploadFile.UploadFileID
                        };
                        var path = Path.Combine(Server.MapPath("~/Content/Images/AttachFiles/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);

                        db.Entry(fileDetail).State = EntityState.Added;
                    }
                }

                db.Entry(uploadFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", uploadFile.PositionID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName", uploadFile.EmployeeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", uploadFile.StudentID);
            return View(uploadFile);
        }

        // GET: UploadFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadFile uploadFile = db.UploadFiles.Find(id);
            if (uploadFile == null)
            {
                return HttpNotFound();
            }
            return View(uploadFile);
        }

        // POST: UploadFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UploadFile uploadFile = db.UploadFiles.Find(id);
            db.UploadFiles.Remove(uploadFile);
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
