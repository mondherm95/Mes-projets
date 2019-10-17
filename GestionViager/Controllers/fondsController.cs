using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionViager.Models;

namespace GestionViager.Controllers
{
    public class fondsController : Controller
    {
        private GestionImmobilierEntities db = new GestionImmobilierEntities();

        // GET: fonds
        public ActionResult Index()
        {
            return View(db.fonds.ToList());
        }

        // GET: fonds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fonds fonds = db.fonds.Find(id);
            if (fonds == null)
            {
                return HttpNotFound();
            }
            return View(fonds);
        }

        // GET: fonds/Create
        public ActionResult CreateFonds(long id)
        {
            ViewData["id"] = id;

            return View();
        }

        // POST: fonds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFonds([Bind(Include = "id,fonds1")] fonds fonds,int? m)
        {

            try { 

        
                db.fonds.Add(fonds);
                db.SaveChanges();
                return RedirectToAction("Details", "folders", new { id = m.Value });

            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: fonds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fonds fonds = db.fonds.Find(id);
            if (fonds == null)
            {
                return HttpNotFound();
            }
            return View(fonds);
        }

        // POST: fonds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fonds1")] fonds fonds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fonds).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fonds);
        }

        // GET: fonds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fonds fonds = db.fonds.Find(id);
            if (fonds == null)
            {
                return HttpNotFound();
            }
            return View(fonds);
        }

        // POST: fonds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fonds fonds = db.fonds.Find(id);
            db.fonds.Remove(fonds);
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
