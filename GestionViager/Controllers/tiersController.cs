using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionViager.Models;
using System.Dynamic;
using System.Globalization;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace GestionViager.Controllers
{
    public class tiersController : Controller
    {
        private GestionImmobilierEntities db = new GestionImmobilierEntities();


      

        public Double? esperance(int age1,int age2,int id_folder)
        {
            double? esp=0;
            List<tiers> l = db.tiers.Where(m => m.id_folder == id_folder).ToList();
            tiersconjointModel t = new tiersconjointModel();
            string queryString = "";
            t.tiers = new tiers();
            t.conjoint = new tiers();

            folder f = db.folder.Find(id_folder);
            
            if (l != null)
            {
                if (l.Count == 2)
                {

                    t.tiers = l[0];
                        t.conjoint = l[1];


                        if (t.tiers.gender.Equals("Homme") && t.conjoint.gender.Equals("Femme"))
                        {

                        queryString = "select Espérance from  " + f.version1.version1 + " where [HOMME - âge] =" +age1 + " and [FEMME - âge] =" + age2 + " ;";

                        using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                        {
                            var command = new SqlCommand(queryString, connection);

                            connection.Open();


                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                esp = reader.GetDouble(0);

                            }
                        }


                      
                        }
                      
               
                }
                if ( age2 == 0)
                {

                    t.tiers = l[0];

                        if (t.tiers.gender.Equals("Homme"))
                        {
                        queryString = "select Espérance from  " + f.version1.version1 + " where [HOMME - âge] =" + age1 + " and [FEMME - âge] IS NULL  ;";

                        using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                        {
                            var command = new SqlCommand(queryString, connection);

                            connection.Open();


                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                esp = reader.GetDouble(0);

                            }
                        }
                    }
                        else if (t.tiers.gender.Equals("Femme"))
                        {
                        queryString = "select Espérance from  " + f.version1.version1 + " where [HOMME - âge] IS NULL and [FEMME - âge] =" + age1 + " ;";

                        using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                        {
                            var command = new SqlCommand(queryString, connection);

                            connection.Open();


                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                esp = reader.GetDouble(0);

                            }
                        }
                    }


                    }
                }

         return esp;

        }
     
        // GET: tiers
        public ActionResult Save(long id)
        {
            List<string> sexe = new List<string> { "Homme", "Femme" };

            ViewData["sexe"] = new SelectList(sexe);
            ViewData["id"] = id;

            folder f = db.folder.Find(id);

            List<tiers> l = db.tiers.Where(m => m.id_folder == id).ToList();
            tiersconjointModel t = new tiersconjointModel();
            string queryString = " ";

            t.tiers = new tiers();
            t.conjoint = new tiers();

            if (l != null)
            {

                if (l.Count == 2)

                {

                    t.tiers = l[0];
                    t.conjoint = l[1];



                    DateTime dt = DateTime.ParseExact(t.tiers.birth_date.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    string birth_tiers = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    ViewData["naissanceTier"] = birth_tiers;

                    DateTime d = DateTime.ParseExact(t.conjoint.birth_date.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    string birth_Conjoint = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    ViewData["naissanceConjoint"] = birth_Conjoint;

                    if (t.tiers.gender.Equals("Homme") && t.conjoint.gender.Equals("Femme"))
                    {

                         queryString = "select Espérance from  " +f.version1.version1+ " where [HOMME - âge] =" + t.tiers.age+ " and [FEMME - âge] =" + t.conjoint.age+" ;";

                        using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                        {
                            var command = new SqlCommand(queryString, connection);

                            connection.Open();


                           SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                ViewData["esperance"] = reader.GetDouble(0);

                            }

                        }                            

                        
                    }
                    else if (t.tiers.gender.Equals("Femme") && t.conjoint.gender.Equals("Homme"))
                    {
                        

                        queryString = "select Espérance from  " + f.version1.version1 + " where [HOMME - âge] =" + t.conjoint.age + " and [FEMME - âge] =" + t.tiers.age + " ;";

                        using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                        {
                            var command = new SqlCommand(queryString, connection);

                            connection.Open();


                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                ViewData["esperance"] = reader.GetDouble(0);

                            }

                        }

                    }


                }
                else if (l.Count == 1)

                {
                    t.tiers = l[0];

                    DateTime dt = DateTime.ParseExact(t.tiers.birth_date.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    string birth_tiers = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    ViewData["naissanceTier"] = birth_tiers;

                    if (t.tiers.gender.Equals("Homme"))
                    {
                        queryString = "select Espérance from  " + f.version1.version1 + " where [HOMME - âge] =" + t.tiers.age + " and [FEMME - âge] IS NULL ;";

                        using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                        {
                            var command = new SqlCommand(queryString, connection);

                            connection.Open();


                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                ViewData["esperance"] = reader.GetDouble(0);

                            }

                        }
                    }
                    else if (t.tiers.gender.Equals("Femme"))
                    {
                        queryString = "select Espérance from  " + f.version1.version1 + " where [HOMME - âge] IS NULL  and [FEMME - âge] =" + t.tiers.age + " ;";

                        using (var connection = new SqlConnection("data source=DESKTOP-KV8OTIG;initial catalog=GestionImmobilier;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                        {
                            var command = new SqlCommand(queryString, connection);

                            connection.Open();


                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                ViewData["esperance"] = reader.GetDouble(0);

                            }

                        }
                    }


                }

            }
            return View(t);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "tiers,conjoint")] tiersconjointModel tier, long phone)
        {

         

            tiers t = tier.tiers;
            tiers conjoint = tier.conjoint;
            t.phone = phone; 
            System.Diagnostics.Debug.WriteLine("phone  "+t.phone );
            System.Diagnostics.Debug.WriteLine("phone2  " + t.phone2);


            try
            {
                DateTime localDate = DateTime.Now;

                if (t.id == 0)
                {
                   

                    t.age = localDate.Year - t.birth_date.Value.Year;


                    t.id = t.id + 1;

                    db.tiers.Add(t);


                    db.SaveChanges();


                }
                else
                {


                    t.age = localDate.Year - t.birth_date.Value.Year;
                    db.Entry(t).State = EntityState.Modified;
                    db.SaveChanges();

                }

             


                if (conjoint.id == 0 && tier.conjoint.lastname != null && tier.conjoint.firstname != null && tier.conjoint.phone != null && tier.conjoint.gender != null && tier.conjoint.phone2 != null && tier.conjoint.birth_date != null)
                {

                    conjoint.id_folder = t.id_folder;
                    conjoint.age = localDate.Year - conjoint.birth_date.Value.Year;

                    db.tiers.Add(conjoint);
                    db.SaveChanges();

                }
                else if (conjoint.id != 0 && tier.conjoint.lastname != null && tier.conjoint.firstname != null && tier.conjoint.phone != null && tier.conjoint.gender != null && tier.conjoint.phone2 != null && tier.conjoint.birth_date != null)
                {

                 
                    conjoint.id_folder = t.id_folder;
                    conjoint.age = localDate.Year - conjoint.birth_date.Value.Year;
                    db.Entry(conjoint).State = EntityState.Modified;
                    db.SaveChanges();

                }


                return RedirectToAction("Details", "folders", new { id = t.id_folder });


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







   
    }
}
