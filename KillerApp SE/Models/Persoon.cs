using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp_SE.Models
{
    public class Persoon
    {
        protected string naam; protected string adres;

        public string Naam
        {
            get { return naam; }
            set { naam = value; }
        }
        public string Adres
        {
            get { return adres; }
            set { adres = value; }
        }
        public Persoon(string naam, string adres)
        {
            this.naam = naam;
            this.adres = adres;
        }

        public override string ToString()
        {
            return this.naam;
        }
    }
}