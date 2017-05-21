using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerApp_SE.SQLRepository;
using KillerApp_SE.Controllers;
using KillerApp_SE.Models;

namespace KillerApp_SE.Controllers
{
    public class InlogController : Controller
    {
        Bibliotheek bib = new Bibliotheek();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            if (!string.IsNullOrEmpty(fc["gebruikernaam"]) && !string.IsNullOrEmpty(fc["wachtwoord"]))
            {
                if (bib.Login(fc["gebruikernaam"], fc["wachtwoord"]) == true)
                {
                    Session["Gebruikernaam"] = fc["gebruikernaam"];
                }
            }
            else ViewBag.Message = "Gebruikersgegevens komen niet overeen."; 
            return RedirectToAction("Index", "Home");
        }
    }
}