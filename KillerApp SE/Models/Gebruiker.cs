using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp_SE.Models
{
    public class Gebruiker : Persoon
    {
        private bool ingelogd = false;
        private int gebruikerID;
        private string gebruikernaam;
        private string wachtwoord;
        private DateTime geboortedatum;

        public bool Ingelogd
        {
            get { return ingelogd; }
            set { ingelogd = value; }
        }
        public string Gebruikernaam
        {
            get { return gebruikernaam; }
            set { gebruikernaam = value; }
        }

        public Gebruiker(string naam, string adres, DateTime geboortedatum) : base(naam, adres)
        {
            this.geboortedatum = geboortedatum;
            this.naam = naam;
            this.adres = adres;
        }
    }
}