using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KillerApp_SE.Models;
using KillerApp_SE.SQLRepository;

namespace KillerApp_SE.Controllers
{
    public class GebruikersController : Controller
    {
        Repository rep = new Repository();
        Bibliotheek bib = new Bibliotheek();

        public ActionResult GetMijnGegevens(string gebruikernaam)
        {
            List<string> gebruikerInfo;
            gebruikerInfo = rep.GetGebruikersGegevens("Admin");
            ViewBag.Naam = gebruikerInfo.ElementAt(0);
            ViewBag.Adres = gebruikerInfo.ElementAt(1);
            ViewBag.Geboortedatum = gebruikerInfo.ElementAt(2);
            return View();
        }
        public ActionResult GebruikerToevoegen()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GebruikerToevoegen(FormCollection fc)
        {
            string gebruikernaam = fc["Naam"];
            string wachtwoord = fc["Wachtwoord"];
            string naam = fc["Naam"];
            string adres = fc["Adres"];
            string geboortedatum = fc["Geboortedatum"];
            rep.GebruikerToevoegen(gebruikernaam, wachtwoord, naam, adres, geboortedatum);
            return View();
        }
        //[HttpGet]
        //public ActionResult WijzigMijnGegevens(string naam, string adres, string geboortedatum)
        //{
        //    rep.WijzigMijnGegevens("Admin", "naam", "adres", "geboortedatum");
        //    return View();
        //}
        //public ActionResult GebruikerBeheren()
        //{
        //    List<string> gebruikerslijst = new List<string>();
        //    gebruikerslijst = rep.GetGebruikerslijst();
        //    ViewBag.Gebruikerslijst = gebruikerslijst;
        //    return View();
        //}
        //public ActionResult GebruikerVerwijderen(string gebruikernaam)
        //{
        //    rep.GebruikerVerwijderen(gebruikernaam);
        //    return View();
        //}
        //public ActionResult GebruikerResetten(string gebruikernaam, string wachtwoord, string wachtwoordBevestig)
        //{
        //    if (wachtwoord == wachtwoordBevestig)
        //    {
        //        rep.GebruikerResetten(gebruikernaam, wachtwoord);
        //    }
        //    return View();
        //}
    }
}