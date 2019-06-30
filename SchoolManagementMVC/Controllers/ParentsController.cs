using System;
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
    public class ParentsController : Controller
    {
        private SchoolManagementMVCContext db = new SchoolManagementMVCContext();

        // GET: Parents
        public ActionResult Index()
        {
            var parents = db.Parents.Include(p => p.DocumentType).Include(p => p.Gender);
            return View(parents.ToList());
        }

        // GET: Parents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.Parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // GET: Parents/Create
        public ActionResult Create()
        {
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description");
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description");
            return View();
        }

        // POST: Parents/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParentID,FirstName,LastName,BirthDate,Address,Phone,Email,DocumentTypeID,DocumentNumber,GenderID")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Parents.Add(parent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", parent.DocumentTypeID);
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description", parent.GenderID);
            return View(parent);
        }

        // GET: Parents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.Parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", parent.DocumentTypeID);
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description", parent.GenderID);
            return View(parent);
        }

        // POST: Parents/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParentID,FirstName,LastName,BirthDate,Address,Phone,Email,DocumentTypeID,DocumentNumber,GenderID")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", parent.DocumentTypeID);
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "Description", parent.GenderID);
            return View(parent);
        }

        // GET: Parents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.Parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parent parent = db.Parents.Find(id);
            db.Parents.Remove(parent);
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
