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
    public class GendersController : Controller
    {
        private SchoolManagementMVCContext db = new SchoolManagementMVCContext();

        // GET: Genders
        public ActionResult Index()
        {
            return View(db.Genders.ToList());
        }

        // GET: Genders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // GET: Genders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenderID,Description")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                db.Genders.Add(gender);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gender);
        }

        // GET: Genders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // POST: Genders/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenderID,Description")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gender).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gender);
        }

        // GET: Genders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // POST: Genders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gender gender = db.Genders.Find(id);
            db.Genders.Remove(gender);
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
