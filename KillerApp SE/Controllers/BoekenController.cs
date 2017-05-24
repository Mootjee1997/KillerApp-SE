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
            }
            else ViewData["boeken"] = bib.GetBoekenLijst();
            return View();
        }
        [HttpGet]
        public ActionResult LeenBoek(string id)
        {
            ViewData["boeken"] = bib.GetBoekenLijst();
            foreach (Boek boek in bib.GetMijnBoeken(Session["Gebruikernaam"].ToString()))
            {
                if (boek.Titel == id)
                {
                    ViewBag.Message = "Dit boek hebt u al geleend.";
                    return View("GetBoekenLijst");
                }
            }
            bib.LeenBoek(Session["Gebruikernaam"].ToString(), id);
            ViewData["boeken"] = bib.GetBoekenLijst();
            return View("GetBoekenLijst");
        }
        [HttpGet]
        public ActionResult RetourBoek(string id)
        {
            bib.RetourBoek(Session["Gebruikernaam"].ToString(), id);
            ViewData["boeken"] = bib.GetMijnBoeken(Session["Gebruikernaam"].ToString());
            return View("MijnBoeken");
        }
        [HttpGet]
        public ActionResult MijnBoeken()
        {
            ViewData["boeken"] = bib.GetMijnBoeken(Session["Gebruikernaam"].ToString());
            return View();
        }
    }
}