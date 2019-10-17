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
    public class affectationsController : Controller
    {
        private GestionImmobilierEntities db = new GestionImmobilierEntities();

        // GET: affectations
        public ActionResult Index()
        {

            var affectation = db.affectation.Include(a => a.fonds1).Include(a => a.Propriete1);
            return View(affectation.ToList());
        }

        // GET: affectations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            affectation affectation = db.affectation.Find(id);
            if (affectation == null)
            {
                return HttpNotFound();
            }
            return View(affectation);
        }

        // GET: affectations/Create
        public ActionResult CreateAffectation(long id)
        {

            Propriete p = db.Propriete.Where(m => m.id_folder == id).FirstOrDefault();
            affectation affectation = new affectation();
            
            if (p != null)
            {
                ViewData["id"] = p;

                var somme = db.affectation.Where(m => m.propriete == p.id).Select(m => m.value).Sum();

                var max = 100 - somme;

                if(max>=0)
                {
                    ViewData["max"] = max;

                }
                ViewBag.propriete = new SelectList(db.Propriete, "id", "name");
            }
            ViewBag.fonds = new SelectList(db.fonds, "id", "fonds1");
            
            return View();
        }

        // POST: affectations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAffectation([Bind(Include = "value,fonds,propriete,id")] affectation affectation)
        {
            try
            {
                Propriete p = db.Propriete.Where(m => m.id == affectation.propriete).FirstOrDefault();

                System.Diagnostics.Debug.WriteLine("prop   " + p.id);


                var somme = db.affectation.Where(m => m.propriete == affectation.propriete).Select(m => m.value).Sum();

                

                

                System.Diagnostics.Debug.WriteLine("val   " + affectation.value);
                if(somme == null)
                {

                    somme = 0; 

                }

                var max = 100 - somme;

                if (max >= affectation.value)
                {

                    System.Diagnostics.Debug.WriteLine("aff   " + affectation.propriete);

                    db.affectation.Add(affectation);
                    db.SaveChanges();
                    
                }

                return RedirectToAction("Details", "folders", new { id = p.id_folder });

            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: affectations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            affectation affectation = db.affectation.Find(id);
            if (affectation == null)
            {
                return HttpNotFound();
            }
            ViewBag.fonds = new SelectList(db.fonds, "id", "fonds1", affectation.fonds);
            ViewBag.propriete = new SelectList(db.Propriete, "id", "name", affectation.propriete);
            return View(affectation);
        }

        // POST: affectations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "value,fonds,propriete,id")] affectation affectation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(affectation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fonds = new SelectList(db.fonds, "id", "fonds1", affectation.fonds);
            ViewBag.propriete = new SelectList(db.Propriete, "id", "name", affectation.propriete);
            return View(affectation);
        }

        // GET: affectations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            affectation affectation = db.affectation.Find(id);
            if (affectation == null)
            {
                return HttpNotFound();
            }
            return View(affectation);
        }

        // POST: affectations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            affectation affectation = db.affectation.Find(id);
            db.affectation.Remove(affectation);
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
