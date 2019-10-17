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
    public class UsersController : Controller
    {
        private GestionImmobilierEntities db = new GestionImmobilierEntities();

        // GET: Users
        public ActionResult Index()
        {
            var user = db.User.Include(u => u.Role1);
            return View(user.ToList());
        }


        public ActionResult Inscription()
        {

            return View();
        }


        public JsonResult Verifier([Bind(Include = "firstname,lastname,password,email")] User user)
        {
            string valide = "false";


            User u = db.User.Where(m => m.email == user.email).FirstOrDefault();



            if (u == null)
            {
                valide = "true";

                Session["id"] = u.id;
                Session["prenom"] = u.firstname;
                Session["nom"] = u.lastname;

                db.User.Add(user);
                db.SaveChanges();

            }



            return Json(new
            {
                reponse = valide,
                firstname = user.firstname,
                email = user.email,
                lastname = user.lastname,
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Login()
        {

            return View();
        }


        public ActionResult Deconnexion(int id)
        {

            User u = db.User.Find(id);

            Session.Abandon();

            return RedirectToAction("Login", "Users");
        }

        public JsonResult VerifLogin([Bind(Include = "password,email")] User user)
        {


            string valide = "false";
            User u = db.User.Where(m => m.email == user.email && m.password == user.password).FirstOrDefault();

            if (u != null)
            {
                Session["id"] = u.id;
                Session["prenom"] = u.firstname;
                Session["nom"] = u.lastname;

                valide = "true";
            }


            return Json(new
            {
                reponse = valide,
                password = user.password,
                email = user.email,
            }, JsonRequestBehavior.AllowGet);


        }


    }
}
