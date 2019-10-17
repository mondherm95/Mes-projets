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
    public class foldersController : Controller
    {
        private GestionImmobilierEntities db = new GestionImmobilierEntities();

        // GET: folders
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {

                return RedirectToAction("Login", "Users");
            }

            return View(db.folder.ToList());
        }

        // GET: folders/Details/5
        public ActionResult Details(long? id)
        {
            if (Session["id"] == null)
            {

                return RedirectToAction("Login", "Users");
            }

            List<tiers> l = db.tiers.Where(m => m.id_folder == id).ToList();
            tiersconjointModel t = new tiersconjointModel();

            t.tiers = new tiers();
            t.conjoint = new tiers();


            if (l.Count != 0)
            {
                ViewData["tier"] = l[0];

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            folder folder = db.folder.Find(id);
            if (folder == null)
            {
                return HttpNotFound();
            }
            return View(folder);
        }

        // GET: folders/Create
        public ActionResult Create()
        {
            if (Session["id"] == null)
            {

                return RedirectToAction("Login", "Users");
            }

            ViewBag.version = new SelectList(db.version, "id", "version1");

            return View();
        }

        // POST: folders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,contributor,version,folder_code,created_at,created_by,state")] folder folder)
        {
            if (ModelState.IsValid)
            {
                DateTime localDate = DateTime.Now;
                folder.created_at = localDate;
                folder.state = "En cours";
                folder.folder_code = "CI" + folder.folder_code;
                folder.created_by = Session["prenom"].ToString();
                db.folder.Add(folder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(folder);
        }

        // GET: folders/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            folder folder = db.folder.Find(id);
            if (folder == null)
            {
                return HttpNotFound();
            }
            return View(folder);
        }

        // POST: folders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,contributor,folder_code,created_at,created_by,state")] folder folder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(folder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(folder);
        }

        // GET: folders/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            folder folder = db.folder.Find(id);
            if (folder == null)
            {
                return HttpNotFound();
            }
            return View(folder);
        }

        // POST: folders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            folder folder = db.folder.Find(id);
            db.folder.Remove(folder);
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
