using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KillerApp_SE.Controllers
{
    public class MijnBoekenController : Controller
    {
        // GET: MijnBoeken
        public ActionResult Index()
        {
            return View();
        }
    }
}