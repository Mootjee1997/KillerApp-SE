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
        private string gebruikernaam;
        public string Gebruikernaam { get { return gebruikernaam; } set { gebruikernaam = value; } }
        public List<Gebruiker> gebruikers = new List<Gebruiker>();
        public List<Boek> boeken = new List<Boek>();
        Repository rep = new Repository();

        public string GetStatus(string gebruikernaam)
        {
            string status = "";
            GetGebruikersLijst();
            foreach (Gebruiker g in gebruikers)
            {
                if (g.Gebruikernaam == gebruikernaam)
                {
                    status = g.Status;
                    return status;
                }
            }
            return status;
        }
        public Gebruiker Zoekgebruiker(string gebruikernaam)
        {
            GetGebruikersLijst();
            Gebruiker gebruiker = new Gebruiker("", "", "", "", "", "");
            foreach (Gebruiker g in gebruikers)
            {
                if (g.Gebruikernaam == gebruikernaam)
                {
                    gebruiker = g;
                    return gebruiker;
                }
            }
            return gebruiker;
        }
        public void VerwijderGebruiker(string gebruikernaam)
        {
            GetGebruikersLijst();
            foreach (Gebruiker g in gebruikers)
            {
                if (g.Gebruikernaam == gebruikernaam)
                {
                    rep.GebruikerVerwijderen(gebruikernaam);
                }
            }
            rep.GetGebruikersLijst();
        }
        public bool Login(string gebruikernaam, string wachtwoord)
        {
            return rep.Login(gebruikernaam, wachtwoord);
        }
        //Gebruiker toevoegen
        public void GebruikerToevoegen(string gebruikernaam, string wachtwoord, string naam, string adres, string geboortedatum)
        {
            rep.GebruikerToevoegen(gebruikernaam, wachtwoord, naam, adres, geboortedatum, "Gebruiker");
            rep.GetGebruikersLijst();
        }
        //Gegevens van een gebruiker wijzigen
        public void WijzigGegevens(string gebruikernaam, string naam, string adres, string geboortedatum, string wachtwoord)
        {
            rep.WijzigGegevens(gebruikernaam, naam, adres, geboortedatum, wachtwoord);
            rep.GetGebruikersLijst();
        }
        public List<Gebruiker> GetGebruikersLijst()
        {
            gebruikers = rep.GetGebruikersLijst();
            return gebruikers;
        }
        public List<Boek> GetBoekenLijst()
        {
            boeken = rep.GetBoekenlijst();
            return boeken;
        }
        public void LeenBoek(string gebruikernaam, string titel)
        {
            GetBoekenLijst();
            foreach (Boek boek in boeken)
            {
                if (boek.Titel == titel)
                {
                    if (boek.AantalBeschikbaar > 0)
                    {
                        rep.LeenBoek(gebruikernaam, titel);
                        rep.UpdateBoek(titel);
                    }
                }
            }
        }

        public List<Boek> GetMijnBoeken(string gebruikernaam)
        {
            return rep.GetMijnBoeken(gebruikernaam);
        }
    }
}