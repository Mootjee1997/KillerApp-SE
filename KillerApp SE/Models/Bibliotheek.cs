using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerApp_SE.SQLRepository;
using KillerApp_SE.Controllers;
using KillerApp_SE.Models;

namespace KillerApp_SE.Models
{
    public class Bibliotheek
    {
        List<Gebruiker> gebruikers = new List<Gebruiker>();
        List<Boek> boeken = new List<Boek>();
    }
}