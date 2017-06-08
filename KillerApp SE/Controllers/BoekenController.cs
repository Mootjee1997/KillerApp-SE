using System.Web.Mvc;
using KillerApp_SE.Models;

namespace KillerApp_SE.Controllers
{
    public class BoekenController : Controller
    {
        //Haalt de boekenlijst op
        [HttpGet]
        public ActionResult GetBoekenLijst(FormCollection fc)
        {
            Bibliotheek.GetBoekenLijst();
            //Kijkt of er een zoektitel is waar op gefilterd moet worden
            if (Request.QueryString["ZoekTitel"] != null)
            {
                ViewData["boeken"] = Bibliotheek.ZoekBoek(Request.QueryString["ZoekTitel"]);
                ViewBag.Message = "Boeken voor zoekterm: " + "'" + Request.QueryString["ZoekTitel"] + "'";
            }
            //Als er geen zoekfilter is geeft die de volledige boekenlijst weer
            else ViewData["boeken"] = Bibliotheek.Boeken;
            return View();
        }
        //Methode voor het lenen van een boek
        [HttpGet]
        public ActionResult LeenBoek(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (!Bibliotheek.GetGebruiker(Session["Gebruikernaam"].ToString()).Boeken.Contains(Bibliotheek.GetBoek(id)))
                {
                    Bibliotheek.GetGebruiker(Session["Gebruikernaam"].ToString()).LeenBoek(Bibliotheek.GetBoek(id));
                }
                ViewData["boeken"] = Bibliotheek.Boeken;
                return View("GetBoekenLijst");
            }
            else return RedirectToAction("Login", "Home");          
        }
        //Retourneren van boeken
        [HttpGet]
        public ActionResult RetourBoek(string id, string gebruiker)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (gebruiker == null)
                {
                    ViewBag.Title = "Mijn boeken";
                    Bibliotheek.GetGebruiker(Session["Gebruikernaam"].ToString()).RetourBoek(Bibliotheek.GetBoek(id));
                    ViewData["boeken"] = Bibliotheek.GetGebruiker(Session["Gebruikernaam"].ToString()).Boeken;
                    return View("MijnBoeken");
                }
                else
                {
                    ViewBag.Title = Bibliotheek.GetGebruiker(gebruiker).Naam + "'s " + "boeken";
                    Bibliotheek.GetGebruiker(gebruiker).RetourBoek(Bibliotheek.GetBoek(id));
                    ViewData["boeken"] = Bibliotheek.GetGebruiker(gebruiker).Boeken;
                    return View("MijnBoeken");
                }
            }
            else return RedirectToAction("Login", "Home");
        }
        //Weergeeft de boekenlijst van een specifieke gebruiker
        [HttpGet]
        public ActionResult MijnBoeken(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (id == null)
                {
                    ViewBag.Title = "Mijn boeken";
                    ViewData["boeken"] = Bibliotheek.GetGebruiker(Session["Gebruikernaam"].ToString()).Boeken;
                    return View();
                }
                else
                {
                    ViewBag.Title = Bibliotheek.GetGebruiker(id).Naam + "'s " + "boeken";
                    ViewData["boeken"] = Bibliotheek.GetGebruiker(id).Boeken;
                    ViewBag.Gebruiker = id;
                    return View();
                }
            }
            else return RedirectToAction("Login", "Home");
        }
    }
}