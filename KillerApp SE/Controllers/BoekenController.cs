using System.Web.Mvc;
using KillerApp_SE.Models;

namespace KillerApp_SE.Controllers
{
    public class BoekenController : Controller
    {
        Bibliotheek bib = new Bibliotheek();

        //Haalt de boekenlijst op
        [HttpGet]
        public ActionResult GetBoekenLijst(FormCollection fc)
        {
            //Kijkt of er een zoektitel is waar op gefilterd moet worden
            if (Request.QueryString["ZoekTitel"] != null)
            {
                ViewData["boeken"] = bib.ZoekBoek(Request.QueryString["ZoekTitel"]);
                ViewBag.Message = "Boeken voor zoekterm: " + "'" + Request.QueryString["ZoekTitel"] + "'";
            }
            //Als er geen zoekfilter is geeft die de volledige boekenlijst weer
            else ViewData["boeken"] = bib.GetBoekenLijst();
            return View();
        }
        //Methode voor het lenen van een boek
        [HttpGet]
        public ActionResult LeenBoek(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                //Kijkt of het boek al in je lijst met boeken zit, je mag boeken maar 1 x tegelijk lenen
                if (bib.ZoekMijnBoekenLijst(Session["Gebruikernaam"].ToString(), id) == false)
                {
                    bib.LeenBoek(Session["Gebruikernaam"].ToString(), id);
                    ViewData["boeken"] = bib.GetBoekenLijst();
                    return View("GetBoekenLijst");
                }
                ViewData["boeken"] = bib.GetBoekenLijst();
                return View("GetBoekenLijst");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        //Retourneren van boeken
        [HttpGet]
        public ActionResult RetourBoek(string id, string gebruiker)
        {
            if (Session["Gebruikernaam"] != null)
            {
                if (gebruiker == null)
                {
                    bib.RetourBoek(Session["Gebruikernaam"].ToString(), id);
                    ViewData["boeken"] = bib.GetMijnBoeken(Session["Gebruikernaam"].ToString());
                    return View("MijnBoeken");
                }
                else
                {
                    bib.RetourBoek(gebruiker, id);
                    ViewData["boeken"] = bib.GetMijnBoeken(gebruiker);
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
                    ViewData["boeken"] = bib.GetMijnBoeken(Session["Gebruikernaam"].ToString());
                    return View();
                }
                else
                {
                    ViewData["boeken"] = bib.GetMijnBoeken(id);
                    ViewBag.Gebruiker = id;
                    return View();
                }
            }
            else return RedirectToAction("Login", "Home");
        }
    }
}