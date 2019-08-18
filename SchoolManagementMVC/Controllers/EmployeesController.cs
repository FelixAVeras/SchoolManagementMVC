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
    public class EmployeesController : Controller
    {
        private SchoolManagementMVCContext db = new SchoolManagementMVCContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.DocumentType).Include(e => e.Positions).Include(e => e.State);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description");
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description");
            ViewBag.StateID = new SelectList(db.States, "StateID", "Description");
            return View();
        }

        // POST: Employees/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeView employeeView)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", employeeView.DocumentTypeID);
                ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", employeeView.PositionID);
                ViewBag.StateID = new SelectList(db.States, "StateID", "Description", employeeView.StateID);

                return View(employeeView);
            }

            // string path = string.Empty;
            string path = Server.MapPath("~/Content/Images/Employees");
            string pic = string.Empty;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (employeeView.ImageUrl != null)
            {
                pic = Path.GetFileName(employeeView.ImageUrl.FileName);
                path = Path.Combine(Server.MapPath("~/Content/Images/Employees"), pic);
                employeeView.ImageUrl.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    employeeView.ImageUrl.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            
            var employee = new Employee
            {
                Address = employeeView.Address,
                BirthDate = employeeView.BirthDate,
                DocumentTypeID = employeeView.DocumentTypeID,
                DocumentType = employeeView.DocumentType,
                DocumentNumber = employeeView.DocumentNumber,
                Email = employeeView.Email,
                EndTime = employeeView.EndTime,
                EntryDate = employeeView.EntryDate,
                FirstName = employeeView.FirstName,
                ImageUrl = pic == string.Empty ? string.Empty : string.Format("~/Content/Images/Employees/{0}", pic),
                LastName = employeeView.LastName,
                Phone = employeeView.Phone,
                PositionID = employeeView.PositionID,
                Positions = employeeView.Positions,
                Remarks = employeeView.Remarks,
                Salary = employeeView.Salary,
                StartTime = employeeView.StartTime,
                StateID = employeeView.StateID,
                State = employeeView.State
            };

            db.Employees.Add(employee);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null)
                {
                    ViewBag.Error = ex.Message;
                }

                ViewBag.Error = ex.Message;

                return View(employeeView);
            }

            return RedirectToAction("Index");

        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            var employeeView = new EmployeeView
            {
                EmployeeID = employee.EmployeeID,
                Address = employee.Address,
                BirthDate = employee.BirthDate,
                DocumentType = employee.DocumentType,
                DocumentNumber = employee.DocumentNumber,
                Email = employee.Email,
                EndTime = employee.EndTime,
                EntryDate = employee.EntryDate,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Phone = employee.Phone,
                Positions = employee.Positions,
                Remarks = employee.Remarks,
                Salary = employee.Salary,
                StartTime = employee.StartTime,
                State = employee.State
            };

            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", employee.DocumentTypeID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", employee.PositionID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "Description", employee.StateID);

            return View(employeeView);
        }

        // POST: Employees/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeView employeeView)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeView);
            }

            string path = Server.MapPath("~/Content/Images/Employees");
            string pic = string.Empty;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (employeeView.ImageUrl != null)
            {
                pic = Path.GetFileName(employeeView.ImageUrl.FileName);
                path = Path.Combine(Server.MapPath("~/Content/Images/Employees"), pic);
                employeeView.ImageUrl.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    employeeView.ImageUrl.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            var employee = db.Employees.Find(employeeView.EmployeeID);

            employee.Address = employeeView.Address;
            employee.BirthDate = employeeView.BirthDate;
            employee.DocumentTypeID = employeeView.DocumentTypeID;
            employee.DocumentType = employeeView.DocumentType;
            employee.DocumentNumber = employeeView.DocumentNumber;
            employee.Email = employeeView.Email;
            employee.EndTime = employeeView.EndTime;
            employee.EntryDate = employeeView.EntryDate;
            employee.FirstName = employeeView.FirstName;
            employee.LastName = employeeView.LastName;
            employee.Phone = employeeView.Phone;
            employee.PositionID = employeeView.PositionID;
            employee.Positions = employeeView.Positions;
            employee.Remarks = employeeView.Remarks;
            employee.Salary = employeeView.Salary;
            employee.StartTime = employeeView.StartTime;
            employee.StateID = employeeView.StateID;
            employee.State = employeeView.State;

            if(!string.IsNullOrEmpty(pic))
            {
                employee.ImageUrl = string.Format("~/Content/Images/Employees/{0}", pic);
            }

            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", employee.DocumentTypeID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Description", employee.PositionID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "Description", employee.StateID);

            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
