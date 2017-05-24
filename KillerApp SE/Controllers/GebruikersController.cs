using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KillerApp_SE.Models;

namespace KillerApp_SE.Controllers
{
    public class GebruikersController : Controller
    {
        Bibliotheek bib = new Bibliotheek();

        [HttpGet]
        public ActionResult GebruikerToevoegen()
        {
            if (Session["Gebruikernaam"] != null)
            {
                return View();
            }
            else return RedirectToAction("Login", "Inlog");
        }
        [HttpPost]
        public ActionResult GebruikerToevoegen(FormCollection fc)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (!string.IsNullOrEmpty(fc["Gebruikernaam"].ToString()) && !string.IsNullOrEmpty(fc["Wachtwoord"].ToString()) && !string.IsNullOrEmpty(fc["Naam"].ToString()) && !string.IsNullOrEmpty(fc["Adres"].ToString()) && !string.IsNullOrEmpty(fc["Geboortedatum"].ToString()))
                {
                    bib.GebruikerToevoegen(fc["Gebruikernaam"], fc["Wachtwoord"], fc["Naam"], fc["Adres"], fc["Geboortedatum"]);
                    ViewBag.Message = "Gebruiker succesvol aangemaakt!";
                }
                else ViewBag.Warning = "Voer alle gegevens in aub.";
                return View();
            }
            else return RedirectToAction("Login", "Inlog");
        }
        [HttpGet]
        public ActionResult WijzigMijnGegevens()
        {
            if (Session["Gebruikernaam"] != null)
            {
                return View(bib.Zoekgebruiker(Session["Gebruikernaam"].ToString()));
            }
            else return RedirectToAction("Login", "Inlog");
        }
        [HttpPost]
        public ActionResult WijzigMijnGegevens(FormCollection fc)
        {
            if (Session["Gebruikernaam"] != null)
            {
                bib.WijzigGegevens(Session["Gebruikernaam"].ToString(), fc["Naam"], fc["Adres"], fc["Geboortedatum"], fc["Wachtwoord"]);
                ViewBag.Message = "Uw gegevens zijn succesvol gewijzigd.";
                return View(bib.Zoekgebruiker(Session["Gebruikernaam"].ToString()));
            }
            else return RedirectToAction("Login", "Inlog");
        }
        [HttpGet]
        public ActionResult GebruikerBeheren(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                ViewData["gebruikers"] = bib.GetGebruikersLijst();
                if (id == "Error1")
                {
                    ViewBag.Warning = "Gebruiker heeft nog boeken! Retourneer de boeken om door te gaan."; 
                }
                return View();
            }
            else return RedirectToAction("Login", "Inlog");
        }
        [HttpGet]
        public ActionResult WijzigGegevens(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                return View(bib.Zoekgebruiker(id));
            }
            else return RedirectToAction("Login", "Inlog");
        }
        [HttpPost]
        public ActionResult WijzigGegevens(FormCollection fc, string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                bib.WijzigGegevens(id, fc["Naam"], fc["Adres"], fc["Geboortedatum"], fc["Wachtwoord"]);
                ViewBag.Message = "Gegevens zijn succesvol gewijzigd.";
                return View(bib.Zoekgebruiker(id));
            }
            else return RedirectToAction("Login", "Inlog");
        }
        [HttpGet]
        public ActionResult VerwijderGebruiker(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (bib.GetMijnBoeken(id).Count > 0)
                {
                    return RedirectToAction("GebruikerBeheren", "Gebruikers", new { id = "Error1"});
                }
                else
                {
                    bib.VerwijderGebruiker(id);
                    return RedirectToAction("GebruikerBeheren", "Gebruikers");
                }
            }
            else return RedirectToAction("Login", "Inlog");
        }
        [HttpGet]
        public ActionResult RetourBoekenGebruiker(string id)
        {
            foreach (Boek b in bib.GetMijnBoeken(id))
            {
                bib.RetourBoek(id, b.Titel);
            }
            return RedirectToAction("GebruikerBeheren", "Gebruikers");
        }
        [HttpGet]
        public ActionResult GetBoekenGebruiker(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                ViewData["boeken"] = bib.GetMijnBoeken(id);
                return View();
            }
            else return RedirectToAction("Login", "Inlog");
        }
    }
}