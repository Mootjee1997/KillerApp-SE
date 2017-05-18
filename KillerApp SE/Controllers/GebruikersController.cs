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
            return View();
        }
        //
        [HttpGet]
        public ActionResult WijzigMijnGegevens()
        {
            Gebruiker gebruiker = bib.Zoekgebruiker("Admin");
            return View(gebruiker);
        }
        //
        [HttpPost]
        public ActionResult WijzigMijnGegevens(FormCollection fc)
        {
            bib.WijzigGegevens("Admin", fc["Naam"], fc["Adres"], fc["Geboortedatum"], fc["Wachtwoord"]);
            Gebruiker gebruiker = bib.Zoekgebruiker("Admin");
            return View(gebruiker);
        }
        //
        [HttpGet]
        public ActionResult GebruikerBeheren()
        {
            gebruikers = bib.GetGebruikersLijst();
            ViewData["gebruikers"] = gebruikers;
            return View();
        }
        [HttpGet]
        public ActionResult WijzigGegevens(string id)
        {
            Gebruiker gebruiker = bib.Zoekgebruiker(id);
            return View(gebruiker);
        }
        [HttpPost]
        public ActionResult WijzigGegevens(FormCollection fc, string id)
        {
            bib.WijzigGegevens(id, fc["Naam"], fc["Adres"], fc["Geboortedatum"], fc["Wachtwoord"]);
            Gebruiker gebruiker = bib.Zoekgebruiker(id);
            return View(gebruiker);
        }
    }
}