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
    public class ProprietesController : Controller
    {
        private GestionImmobilierEntities db = new GestionImmobilierEntities();

        // GET: Proprietes
        public ActionResult Index()
        {
            var propriete = db.Propriete.Include(p => p.folder).Include(p => p.Type1);
            return View(propriete.ToList());
        }

        // GET: Proprietes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propriete propriete = db.Propriete.Find(id);
            if (propriete == null)
            {
                return HttpNotFound();
            }
            return View(propriete);
        }

        // GET: Proprietes/Create
        public ActionResult Create(long id)
        {
            ViewBag.type = new SelectList(db.Type, "id", "libelle");

            List<string> bien = new List<string> { "maison", "appartement" };
            ViewData["bien"] = new SelectList(bien);
            ViewData["id"] = id;

            Propriete prop = new Propriete();

          
            var p = db.Propriete.Where(m => m.id_folder == id).FirstOrDefault();
            System.Diagnostics.Debug.WriteLine(p);

            List<affectation> affectation = new List<affectation>();

            if (p != null )
            { 
            affectation = db.affectation.Where(m => m.propriete == p.id).ToList();
            }
            if(affectation.Count!=0)
            {
                ViewData["affectation"] = affectation;
            }
            else if (affectation.Count==0)
            {
                ViewData["affectation"] = new List<affectation>();
            }

            if (p != null)

            {
                var somme = db.Lots.Where(m => m.id_Propriete == p.id).Select(m => m.surface).Sum();
               
                p.area = somme;
                prop = p;
            }

            return View(prop);
        }

        // POST: Proprietes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,region,city,add1,add2,add3,zip_Code,bien,area,type,id_folder")] Propriete propriete)
        {


            try
            {

                if (propriete.id != 0)
                {

                    db.Entry(propriete).State = EntityState.Modified;
                    db.SaveChanges();
                }

                if (propriete.id == 0)
                {
                   

                    db.Propriete.Add(propriete);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "folders", new { id = propriete.id_folder });

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }

        // GET: Proprietes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propriete propriete = db.Propriete.Find(id);
            if (propriete == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_folder = new SelectList(db.folder, "id", "contributor", propriete.id_folder);
            ViewBag.type = new SelectList(db.Type, "id", "libelle", propriete.type);
            return View(propriete);
        }







        // POST: Proprietes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,region,city,add1,add2,add3,zip_Code,bien,area,type,id_folder")] Propriete propriete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propriete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_folder = new SelectList(db.folder, "id", "contributor", propriete.id_folder);
            ViewBag.type = new SelectList(db.Type, "id", "libelle", propriete.type);
            return View(propriete);
        }

        // GET: Proprietes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propriete propriete = db.Propriete.Find(id);
            if (propriete == null)
            {
                return HttpNotFound();
            }
            return View(propriete);
        }

        // POST: Proprietes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Propriete propriete = db.Propriete.Find(id);
            db.Propriete.Remove(propriete);
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
