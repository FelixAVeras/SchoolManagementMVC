﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
            var uploadFiles = db.UploadFiles.Include(u => u.Employees).Include(u => u.Students);
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
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: UploadFiles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UploadingFileID,StudentID,EmployeeID")] UploadFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                db.UploadFiles.Add(uploadFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName", uploadFile.EmployeeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", uploadFile.StudentID);
            return View(uploadFile);
        }

        // POST: UploadFiles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UploadingFileID,StudentID,EmployeeID")] UploadFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uploadFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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