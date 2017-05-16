using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerApp_SE.SQLRepository;
using KillerApp_SE.Controllers;
using KillerApp_SE.Models;

namespace KillerApp_SE.Controllers
{
    public class InlogController : Controller
    {
        Gebruiker gebruiker; 
        Repository rep = new Repository();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorise(string gebruikernaam, string wachtwoord)
        {
            List<string> gebruikerInfo = new List<string>();
            gebruikerInfo = rep.Login(gebruikernaam, wachtwoord);
            gebruiker = new Gebruiker(gebruikerInfo.ElementAt(0), gebruikerInfo.ElementAt(1), Convert.ToDateTime(gebruikerInfo.ElementAt(2)));
            gebruiker.Ingelogd = true;
            
            return View(gebruikernaam);
        }
    }
}