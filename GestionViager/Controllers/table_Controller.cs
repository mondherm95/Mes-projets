using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionViager.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Globalization;

namespace GestionViager.Controllers
{
    public class table_Controller : Controller
    {
        private GestionImmobilierEntities db = new GestionImmobilierEntities();

        // GET: table_
        public ActionResult Index(int id)
        {
            if (Session["id"] == null)
            {

                return RedirectToAction("Login", "Users");
            }

            ViewData["id"] = id;


            List<table_> result = new List<table_>();


            version v = db.version.Find(id);

            string queryString = "select *  from " + v.version1 + "  ;";

            using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                var command = new SqlCommand(queryString, connection);

                connection.Open();


                SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        
                    table_ t = new table_();


                    t.id = reader.GetInt32(6);

                    t.Cible = reader.GetString(0);

                    if (!reader.IsDBNull(1))
                    {
                        t.HOMME___âge = reader.GetDouble(1) as Double?;
                    }
                    if (!reader.IsDBNull(2))
                    {
                        t.FEMME___âge = reader.GetDouble(2);
                    }
                   
                    t.Espérance= reader.GetDouble(3);

                    t.tx_rente= reader.GetDouble(4);

                    t.dt_us_= reader.GetDouble(5);



                    result.Add(t);
                }

                
            }




            return View(result);
        }

        // GET: table_/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_ table_ = db.table_.Find(id);
            if (table_ == null)
            {
                return HttpNotFound();
            }
            return View(table_);
        }

        // GET: table_/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: table_/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cible,HOMME___âge,FEMME___âge,Espérance,tx_rente,dt_us_,F7,F8,F9,F10,F11,F12,F13,F14,int")] table_ table_)
        {
            

            return View(table_);
        }

        // GET: table_/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_ table_ = db.table_.Find(id);
            if (table_ == null)
            {
                return HttpNotFound();
            }
            return View(table_);
        }

        // POST: table_/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cible,HOMME___âge,FEMME___âge,Espérance,tx_rente,dt_us_,F7,F8,F9,F10,F11,F12,F13,F14,int")] table_ table_)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table_).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table_);
        }

        // GET: table_/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_ table_ = db.table_.Find(id);
            if (table_ == null)
            {
                return HttpNotFound();
            }
            return View(table_);
        }

        // POST: table_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            table_ table_ = db.table_.Find(id);
            db.table_.Remove(table_);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult UpdateCustomer(table_ customer , int id)
        {


            version v = db.version.Find(id);

            string queryString = "update  " + v.version1 + " set HOMME___âge=" + customer.HOMME___âge+ " , FEMME___âge =" + customer.FEMME___âge+" where id = "+customer.id+";";

            using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                var command = new SqlCommand(queryString, connection);

                connection.Open();
                command.ExecuteNonQuery();

            }



            return new EmptyResult();
        }

        public JsonResult ChangeUser(table_ model, GridView gridContent)
        {
            

            foreach (GridViewRow row in gridContent.Rows)
            {

                System.Diagnostics.Debug.WriteLine("Alooo ijareb  ");

              System.Diagnostics.Debug.WriteLine("Mondher ijareb  " +row.FindControl("HOMME___âge"));


            }

            // Update model to your db  
            string message = "Success";
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Inserer(table_ customer,int id)
        {
            version v = db.version.Find(id);

            string esp = customer.Espérance.ToString().Replace(",", ".");
            string us = customer.dt_us_.ToString().Replace(",",".");
            string rente = customer.tx_rente.ToString().Replace(",", ".");

            string queryString = "insert into  " + v.version1 + " (Cible,HOMME___âge,FEMME___âge,Espérance,tx_rente,dt_us_)VALUES('" + customer.Cible+"',"+customer.HOMME___âge+","+customer.FEMME___âge+","+ esp+ ","+ rente + ","+ us+ ") ;";

            using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                var command = new SqlCommand(queryString, connection);

                connection.Open();
                command.ExecuteNonQuery();

            }

            
            return Json(customer);

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
