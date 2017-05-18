using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp_SE.Models
{
    public class Gebruiker : Persoon
    {
        private string gebruikernaam;
        private string wachtwoord;
        private string geboortedatum;
        private string status;

        public string Gebruikernaam
        {
            get { return gebruikernaam; }
            set { gebruikernaam = value; }
        }
        public string Wachtwoord
        {
            get { return wachtwoord; }
            set { wachtwoord = value; }
        }
        public string Geboortedatum
        {
            get { return geboortedatum; }
            set { geboortedatum = value; }
        }
        public string Status
        {
            get { return geboortedatum; }
        }

        public Gebruiker(string gebruikernaam, string wachtwoord, string naam, string adres, string geboortedatum, string status) : base(naam, adres)
        {
            this.gebruikernaam = gebruikernaam;
            this.wachtwoord = wachtwoord;
            this.geboortedatum = geboortedatum;
            this.naam = naam;
            this.adres = adres;
            this.status = status;
        }
    }
}