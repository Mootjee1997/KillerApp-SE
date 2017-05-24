using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KillerApp_SE.Models;
using KillerApp_SE.SQLRepository;

namespace KillerApp_SE.Controllers
{
    public class BoekenController : Controller
    {
        Bibliotheek bib = new Bibliotheek();

        [HttpGet]
        public ActionResult GetBoekenLijst(FormCollection fc)
        {
            if (Request.QueryString["ZoekTitel"] != null)
            {
                ViewData["boeken"] = bib.ZoekBoek(Request.QueryString["ZoekTitel"]);
                ViewBag.Message = "Boeken voor zoekterm: " + "'" + Request.QueryString["ZoekTitel"] + "'";
            }
            else ViewData["boeken"] = bib.GetBoekenLijst();
            return View();
        }
        [HttpGet]
        public ActionResult LeenBoek(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {             
                bib.LeenBoek(Session["Gebruikernaam"].ToString(), id);
                ViewData["boeken"] = bib.GetBoekenLijst();
                return View("GetBoekenLijst");
            }
            else
            {
                return RedirectToAction("Login", "Inlog");
            }
        }
        [HttpGet]
        public ActionResult RetourBoek(string id)
        {
            if (Session["Gebruikernaam"] != null)
            {
                bib.RetourBoek(Session["Gebruikernaam"].ToString(), id);
                ViewData["boeken"] = bib.GetMijnBoeken(Session["Gebruikernaam"].ToString());
                return View("MijnBoeken");
            }
            else return RedirectToAction("Login", "Inlog");
        }
        [HttpGet]
        public ActionResult MijnBoeken()
        {
            if (Session["Gebruikernaam"] != null)
            {
                ViewData["boeken"] = bib.GetMijnBoeken(Session["Gebruikernaam"].ToString());
                return View();
            }
            else return RedirectToAction("Login", "Inlog");
        }
    }
}