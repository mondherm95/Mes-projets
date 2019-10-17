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
    public class LotsController : Controller
    {
        private GestionImmobilierEntities db = new GestionImmobilierEntities();

        // GET: Lots
        public ActionResult Index()
        {
            var lots = db.Lots.Include(l => l.Propriete);
            return View(lots.ToList());
        }

        // GET: Lots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lots lots = db.Lots.Find(id);
            if (lots == null)
            {
                return HttpNotFound();
            }
            return View(lots);
        }

        // GET: Lots/Create
        public ActionResult CreateLots(long id)
        {

            Propriete p = db.Propriete.Where(m=>m.id_folder == id).FirstOrDefault();

            Lots lots = new Lots();
           
            

            if (p != null)
            {
                ViewData["id"] = p;


                ViewBag.id_Propriete = new SelectList(db.Propriete, "id", "nom_Commune");

            }

           

            return View();

        }

        // POST: Lots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLots([Bind(Include = "id,id_Propriete,surface,etage,nature_lot,carrez,balcon,surface_balcon,terrasse,surface_terrasse,jardin,surface_jardin,commentaire")] Lots lots)
        {


            try
            {
                db.Lots.Add(lots);
                db.SaveChanges();
                Propriete p = db.Propriete.Where(m => m.id == lots.id_Propriete).FirstOrDefault();

                

                return RedirectToAction("Details", "folders", new { id = p.id_folder });


            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Lots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lots lots = db.Lots.Find(id);
            if (lots == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Propriete = new SelectList(db.Propriete, "id", "nom_Commune", lots.id_Propriete);
            return View(lots);
        }

        // POST: Lots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_Propriete,surface,etage,nature_lot,carrez,balcon,surface_balcon,terrasse,surface_terrasse,jardin,surface_jardin,commentaire")] Lots lots)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lots).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Propriete = new SelectList(db.Propriete, "id", "nom_Commune", lots.id_Propriete);
            return View(lots);
        }

        // GET: Lots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lots lots = db.Lots.Find(id);
            if (lots == null)
            {
                return HttpNotFound();
            }
            return View(lots);
        }

        // POST: Lots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lots lots = db.Lots.Find(id);
            db.Lots.Remove(lots);
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
