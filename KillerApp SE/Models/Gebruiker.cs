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
        private string geboortedatum;

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
        public string Naam
        {
            get { return naam; }
            set { naam = value; }
        }
        public string Wachtwoord
        {
            get { return wachtwoord; }
            set { wachtwoord = value; }
        }
        public string Adres
        {
            get { return adres; }
            set { adres = value; }
        }
        public string Geboortedatum
        {
            get { return geboortedatum; }
            set { geboortedatum = value; }
        }

        public Gebruiker(string naam, string adres, string geboortedatum) : base(naam, adres)
        {
            this.geboortedatum = geboortedatum;
            this.naam = naam;
            this.adres = adres;
        }
    }
}