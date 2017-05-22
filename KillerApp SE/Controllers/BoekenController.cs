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
        public ActionResult ZoekBoek()
        {
            ViewData["boeken"] = bib.GetBoekenLijst(); 
            return View();
        } 
        [HttpGet]
        public ActionResult LeenBoek(string id)
        {
            bib.LeenBoek(Session["Gebruikernaam"].ToString(), id);
            ViewData["boeken"] = bib.GetBoekenLijst();
            return View("ZoekBoek");
        }
        [HttpGet]
        public ActionResult MijnBoeken()
        {
            ViewData["boeken"] = bib.GetMijnBoeken(Session["Gebruikernaam"].ToString());
            return View();
        }
    }
}