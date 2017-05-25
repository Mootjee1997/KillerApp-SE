using System.Web.Mvc;
using KillerApp_SE.Models;

namespace KillerApp_SE.Controllers
{
    public class HomeController : Controller
    {
        Bibliotheek bib = new Bibliotheek();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            if (!string.IsNullOrEmpty(fc["gebruikernaam"]) && !string.IsNullOrEmpty(fc["wachtwoord"]) && bib.Login(fc["gebruikernaam"], fc["wachtwoord"]) == true)
            {
                Session["Gebruikernaam"] = fc["gebruikernaam"];
                return RedirectToAction("Index", "Home");
            }
            else ViewBag.Message = "Gebruikersgegevens komen niet overeen.";
            return View();
        }
        [HttpGet]
        public ActionResult Logout(FormCollection fc)
        {
            Session["Gebruikernaam"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}