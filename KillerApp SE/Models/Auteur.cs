using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp_SE.Models
{
    public class Auteur : Persoon
    {
        public Auteur(string naam, string adres) : base(naam, adres)
        {
            this.naam = naam;
            this.adres = adres;
        }
    }
}