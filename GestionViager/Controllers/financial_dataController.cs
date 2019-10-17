using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionViager.Models;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace GestionViager.Controllers
{
    public class financial_dataController : Controller
    {
        private GestionImmobilierEntities db = new GestionImmobilierEntities();

        public object MessageBox { get; private set; }

        // GET: financial_data
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {

                return RedirectToAction("Login", "Users");
            }
            return View(db.financial_data.ToList());
        }


        public JsonResult last(Comite comite)
        {

            var com = db.Comite.OrderByDescending(m => m.id).FirstOrDefault();

            financial_data f = new financial_data();
            if (com != null)
            {
                 f = db.financial_data.Where(m => m.id == com.id_finance).FirstOrDefault();

                return Json(new
                {
                    id = com.id,
                    date = com.date_comite,
                    statut = com.statut,
                    etat = com.etat,
                    id_finance = com.id_finance,
                    vf = f.vf,
                    prix_m = f.prix_m,
                    vvo = f.vvo,
                    bouquet = f.bouquet,
                    bouquet_vvl = f.bouquet_vvl,
                    decote_occupation = f.decote_occupation,
                    decote_totale = f.decote_totale,
                    vvl = f.vvl,
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
               
            }, JsonRequestBehavior.AllowGet);

        }



        public JsonResult comi(Comite comite)
        {
            Comite com = db.Comite.Where(m => m.id == comite.id).FirstOrDefault();
            financial_data f = db.financial_data.Where(m => m.id == com.id_finance).FirstOrDefault();

            return Json(new {
                id = com.id,
                date = com.date_comite,
                statut = com.statut,
                etat = com.etat,
                id_finance = com.id_finance,
                vf = f.vf,
                prix_m = f.prix_m,
                vvo = f.vvo,
                bouquet=f.bouquet,
                bouquet_vvl=f.bouquet_vvl,
                decote_occupation=f.decote_occupation,
                decote_totale=f.decote_totale,
                vvl=f.vvl,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult updateComi(int id)
        {
            Comite c = db.Comite.Find(id);

            c.statut = "valide";

            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();

            return new EmptyResult();
        }
        // GET: financial_data/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            financial_data financial_data = db.financial_data.Find(id);
            if (financial_data == null)
            {
                return HttpNotFound();
            }
            return View(financial_data);
        }

        // GET: financial_data/Create
        public ActionResult Create(long id)
        {
            if (Session["id"] == null)
            {

                return RedirectToAction("Login", "Users");
            }

            List<string> type_c = new List<string> { "Inter-Comité", "Comité régulière" };

            ViewData["type"] = new SelectList(type_c);

            ViewData["id_folder"] = id;


            List<Comite> comite = db.Comite.Where(m => m.id_folder == id).OrderByDescending(m => m.id).ToList();
            Propriete p = db.Propriete.Where(m => m.id_folder == id).FirstOrDefault();
            financial_data f = new financial_data();

            List<tiers> l = db.tiers.Where(m => m.id_folder == id).ToList();

            if(comite.Count != 0)
            {
                ViewData["Comite"] = comite;
            }
            else
            {

                ViewData["Comite"] = new List<Comite>();

            }

            if (l != null)
            {
       
                if (l.Count == 2)
                {
                    ViewData["ageTiers"] = l[0].age;
                    ViewData["ageConjoint"] = l[1].age;
                    ViewData["esperance"] = l[0].esperance_vie_actuelle;

                }
                else if (l.Count == 1)
                {
                    ViewData["ageTiers"] = l[0].age;
                    ViewData["esperance"] = l[0].esperance_vie_actuelle;
                }

            }


            if (p != null)
            {

                var somme = db.Lots.Where(m => m.id_Propriete == p.id).Select(m => m.surface).Sum();

                p.area = somme;
                ViewData["id"] = p;
                
            }


            return View();
        }

        // POST: financial_data/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "propriete,id,vvl,prix_m,vvo,vf,decote_occupation,decote_totale,bouquet,bouquet_vvl")] financial_data financial_data,string bouquet_vvl, string prix_m,string decote_totale,string type_c, DateTime date_c)
        {

          

            try
            {

             
              


                float bq_vvl =   float.Parse(bouquet_vvl, CultureInfo.InvariantCulture.NumberFormat);
                float prix = float.Parse(prix_m, CultureInfo.InvariantCulture.NumberFormat);

                float decote = float.Parse(decote_totale, CultureInfo.InvariantCulture.NumberFormat);


                financial_data.bouquet_vvl = bq_vvl;
                financial_data.prix_m = prix;
               financial_data.decote_totale = decote;

                db.financial_data.Add(financial_data);
                db.SaveChanges();

                Propriete p = db.Propriete.Where(m => m.id == financial_data.propriete).FirstOrDefault();
                Comite c = new Comite();

                c.date_comite = date_c;
                c.type_comite = type_c;
                c.etat = "en cours";
                c.statut = "en cours";
                c.id_folder = p.id_folder;
                c.id_finance = financial_data.id;

                db.Comite.Add(c);
                db.SaveChanges();

                return RedirectToAction("Details", "folders", new { id = p.id_folder });

            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: financial_data/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            financial_data financial_data = db.financial_data.Find(id);
            if (financial_data == null)
            {
                return HttpNotFound();
            }
            return View(financial_data);
        }

        // POST: financial_data/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,vvl,prix_m,vvo,vf,decote_occupation,decote_totale,bouquet,bouquet_vvl")] financial_data financial_data)
        {
            if (ModelState.IsValid)
            {
                db.Entry(financial_data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(financial_data);
        }

        // GET: financial_data/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            financial_data financial_data = db.financial_data.Find(id);
            if (financial_data == null)
            {
                return HttpNotFound();
            }
            return View(financial_data);
        }

        // POST: financial_data/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            financial_data financial_data = db.financial_data.Find(id);
            db.financial_data.Remove(financial_data);
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
