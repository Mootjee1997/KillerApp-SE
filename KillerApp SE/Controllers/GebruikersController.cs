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
        Bibliotheek bib = new Bibliotheek();
        List<Gebruiker> gebruikers = new List<Gebruiker>();
        List<string> gebruikerslijst = new List<string>();
        List<string> gebruikerInfo = new List<string>();
        //
        [HttpGet]
        public ActionResult GebruikerToevoegen()
        {
            return View();
        }
        //
        [HttpPost]
        public ActionResult GebruikerToevoegen(FormCollection fc)
        {
            bib.GebruikerToevoegen(fc["Gebruikernaam"], fc["Wachtwoord"], fc["Naam"], fc["Adres"], fc["Geboortedatum"]);
            if (bib.Zoekgebruiker(fc["Gebruikernaam"]) != null)
            {
                ViewBag.Message = "Gebruiker succesvol aangemaakt!";
            }
            return View();
        }
        //
        [HttpGet]
        public ActionResult WijzigMijnGegevens()
        {
            return View(bib.Zoekgebruiker("Admin"));
        }
        //
        [HttpPost]
        public ActionResult WijzigMijnGegevens(FormCollection fc)
        {
            bib.WijzigGegevens("Admin", fc["Naam"], fc["Adres"], fc["Geboortedatum"], fc["Wachtwoord"]);
            ViewBag.Message = "Uw gegevens zijn succesvol gewijzigd.";
            return View(bib.Zoekgebruiker("Admin"));
        }
        //
        [HttpGet]
        public ActionResult GebruikerBeheren()
        {
            ViewData["gebruikers"] = bib.GetGebruikersLijst();
            return View();
        }
        [HttpGet]
        public ActionResult WijzigGegevens(string id)
        {
            return View(bib.Zoekgebruiker(id));
        }
        [HttpPost]
        public ActionResult WijzigGegevens(FormCollection fc, string id)
        {
            bib.WijzigGegevens(id, fc["Naam"], fc["Adres"], fc["Geboortedatum"], fc["Wachtwoord"]);
            return View(bib.Zoekgebruiker(id));
        }
        [HttpGet]
        public void VerwijderGebruiker(string id)
        {
            bib.VerwijderGebruiker(id);
        }
    }
}