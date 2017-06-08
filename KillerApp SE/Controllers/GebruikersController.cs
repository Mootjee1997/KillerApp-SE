using System;
using System.Web.Mvc;
using KillerApp_SE.Models;

namespace KillerApp_SE.Controllers
{
    public class GebruikersController : Controller
    {
        DateTime result = default(DateTime);
        [HttpGet]
        public ActionResult GebruikerToevoegen()
        {
            if (Session["Gebruikernaam"] != null)
            {
                return View();
            }
            else return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult GebruikerToevoegen(FormCollection fc)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (!string.IsNullOrEmpty(fc["Gebruikernaam"].ToString()) && !string.IsNullOrEmpty(fc["Wachtwoord"].ToString()) && !string.IsNullOrEmpty(fc["Naam"].ToString()) && !string.IsNullOrEmpty(fc["Adres"].ToString()) && !string.IsNullOrEmpty(fc["Geboortedatum"].ToString()))
                {
                    if (Bibliotheek.GetGebruiker(fc["Gebruikernaam"]) == null)
                    {
                        if (DateTime.TryParse(fc["Geboortedatum"], out result))
                        {
                            Gebruiker gebruiker = new Gebruiker(fc["Gebruikernaam"].ToString(), fc["Wachtwoord"].ToString(), fc["Naam"].ToString(), fc["Adres"].ToString(), fc["Geboortedatum"].ToString(), "Gebruiker");
                            Bibliotheek.GebruikerToevoegen(gebruiker);
                            ViewBag.Message = "Gebruiker succesvol aangemaakt!";
                            return View("GebruikerBeheren");
                        }
                        else ViewBag.Warning = "Verkeerde input voor geboortedatum! (dd/mm/yyyy).";
                    }
                    else ViewBag.Warning = "Gebruikernaam is bezet.";
                }
                else ViewBag.Warning = "Voer alle gegevens in aub.";
                return View();
            }
            else return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult GebruikerBeheren(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (id == "Error1") ViewBag.Warning = "Gebruiker heeft nog boeken! Retourneer de boeken om door te gaan.";
                return View();
            }
            else return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult WijzigGegevens(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (id != null) return View(Bibliotheek.GetGebruiker(id));
                else return View(Bibliotheek.GetGebruiker(Session["Gebruikernaam"].ToString()));
            }
            else return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult WijzigGegevens(FormCollection fc, string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (id != null)
                {
                    if (DateTime.TryParse(fc["Geboortedatum"], out result))
                    {
                        Bibliotheek.WijzigGegevens(id, fc["Naam"], fc["Adres"], fc["Geboortedatum"], fc["Wachtwoord"]);
                        ViewBag.Message = "Gegevens zijn succesvol gewijzigd.";
                    }
                    else ViewBag.Warning = "Verkeerde input voor geboortedatum! (dd/mm/yyyy).";
                    return View(Bibliotheek.GetGebruiker(id));
                }
                else
                {
                    if (DateTime.TryParse(fc["Geboortedatum"], out result))
                    {
                        Bibliotheek.WijzigGegevens(Session["Gebruikernaam"].ToString(), fc["Naam"], fc["Adres"], fc["Geboortedatum"], fc["Wachtwoord"]);
                        ViewBag.Message = "Uw gegevens zijn succesvol gewijzigd.";
                    }
                    else ViewBag.Warning = "Verkeerde input voor geboortedatum! (dd/mm/yyyy).";
                    return View(Bibliotheek.GetGebruiker(Session["Gebruikernaam"].ToString()));
                }
            }
            else return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult VerwijderGebruiker(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (Bibliotheek.GetGebruiker(id).Boeken.Count > 0)
                {
                    return RedirectToAction("GebruikerBeheren", "Gebruikers", new { id = "Error1" });
                }
                else
                {
                    Bibliotheek.VerwijderGebruiker(id);
                    return RedirectToAction("GebruikerBeheren", "Gebruikers");
                }
            }
            else return RedirectToAction("Login", "Home");
        }
    }
}