using System.Collections.Generic;
using KillerApp_SE.SQLRepository;

namespace KillerApp_SE.Models
{
    public class Gebruiker : Persoon
    {
        private List<Boek> boeken = new List<Boek>();
        private string gebruikernaam;
        private string wachtwoord;
        private string geboortedatum;
        private string status;

        public List<Boek> Boeken
        {
            get { return boeken; }
            set { boeken = value; }
        }
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
            get { return status; }
        }

        public void LeenBoek(Boek boek)
        {
            if (boek.AantalBeschikbaar > 0)
            {
                boek.AantalBeschikbaar -= 1;
                this.boeken.Add(boek);
                Bibliotheek.brep.LeenBoek(this.gebruikernaam, boek);
            }
        }
        public void RetourBoek(Boek boek)
        {
            Boek bk = null;
            boek.AantalBeschikbaar += 1;
            foreach (Boek b in boeken)
            {
                if (b.Titel == boek.Titel) bk = b;
            }
            boeken.Remove(bk);
            //this.boeken.Remove(boek);
            Bibliotheek.brep.RetourBoek(this.gebruikernaam, boek);
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