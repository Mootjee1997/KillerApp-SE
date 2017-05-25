using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp_SE.Models
{
    public class Uitgever : Persoon
    {
        public Uitgever(string naam, string adres) : base(naam, adres)
        {
            this.naam = naam;
            this.adres = adres;
        }
    }
}