using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionViager.Models;
using System.Data.SqlClient;

namespace GestionViager.Controllers
{
    public class versionsController : Controller
    {
        private GestionImmobilierEntities db = new GestionImmobilierEntities();

        // GET: versions
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {

                return RedirectToAction("Login", "Users");
            }

            return View(db.version.ToList());
        }



        [HttpPost]
        public JsonResult InsererVersion(version customer)
        {
            version v = db.version.OrderByDescending(m => m.id).FirstOrDefault();

                db.version.Add(customer);
                db.SaveChanges();


            if (v != null)
            {
                string queryString = "select * into  " + customer.version1 + " from " + v.version1 + " ;";

                using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                {
                    var command = new SqlCommand(queryString, connection);

                    connection.Open();
                    command.ExecuteNonQuery();


                    string q = " alter table " + customer.version1 + "  drop  COLUMN id ;";
                    var d = new SqlCommand(q, connection);

                    d.ExecuteNonQuery();

                    string qs = " ALTER table " + customer.version1 + "  ADD  id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY;";

                    var c = new SqlCommand(qs, connection);
                    c.ExecuteNonQuery();
                }
            }
            else if( v == null)
            {
                string queryString = "select * into  " + customer.version1 + " from table$ ;";

                using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                {
                    var command = new SqlCommand(queryString, connection);

                    connection.Open();
                    command.ExecuteNonQuery();


                    string q = " alter table " + customer.version1 + "  drop  COLUMN id ;";
                    var d = new SqlCommand(q, connection);

                    d.ExecuteNonQuery();

                    string qs = " ALTER table " + customer.version1 + "  ADD  id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY;";

                    var c = new SqlCommand(qs, connection);
                    c.ExecuteNonQuery();
                }
            }

          

            return Json(customer);
           
        }

        // GET: versions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            version version = db.version.Find(id);
            if (version == null)
            {
                return HttpNotFound();
            }
            return View(version);
        }

        // GET: versions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: versions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,version1")] version version)
        {
            if (ModelState.IsValid)
            {
                db.version.Add(version);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(version);
        }

        // GET: versions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            version version = db.version.Find(id);
            if (version == null)
            {
                return HttpNotFound();
            }
            return View(version);
        }

        // POST: versions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,version1")] version version)
        {
            if (ModelState.IsValid)
            {
                db.Entry(version).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(version);
        }

        // GET: versions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            version version = db.version.Find(id);
            if (version == null)
            {
                return HttpNotFound();
            }
            return View(version);
        }

        // POST: versions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            version version = db.version.Find(id);
            db.version.Remove(version);
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
