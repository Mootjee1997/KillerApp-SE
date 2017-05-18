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
        string gebruikernaam;
        public string Gebruikernaam { get { return gebruikernaam; } set { gebruikernaam = value; } }
        public List<Gebruiker> gebruikers = new List<Gebruiker>();
        public List<Boek> boeken = new List<Boek>();
        Repository rep = new Repository();

        public Gebruiker Zoekgebruiker(string gebruikernaam)
        {
            GetGebruikersLijst();
            Gebruiker gebruiker = new Gebruiker("", "", "", "", "", "");
            foreach (Gebruiker g in gebruikers)
            {
                if (g.Gebruikernaam == gebruikernaam)
                {
                    return g;
                }
            }
            return gebruiker;
        }
        //Gebruiker toevoegen
        public void GebruikerToevoegen(string gebruikernaam, string wachtwoord, string naam, string adres, string geboortedatum)
        {
            Gebruiker gebruiker = new Gebruiker(gebruikernaam, wachtwoord, naam, adres, geboortedatum, "Gebruiker");
            rep.GebruikerToevoegen(gebruikernaam, wachtwoord, naam, adres, geboortedatum, "Gebruiker");
        }
        //Gegevens van een gebruiker wijzigen
        public void WijzigGegevens(string gebruikernaam, string naam, string adres, string geboortedatum, string wachtwoord)
        {
            gebruikers = rep.GetGebruikersLijst();
            foreach (Gebruiker g in gebruikers)
            {
                if (g.Gebruikernaam == gebruikernaam)
                {
                    g.Naam = naam;
                    g.Adres = adres;
                    g.Geboortedatum = geboortedatum;
                    g.Wachtwoord = wachtwoord;
                }
            }
            rep.WijzigGegevens(gebruikernaam, naam, adres, geboortedatum, wachtwoord);
        }
        //Gegevens van een gebruiker ophalen
        public List<string> GetGebruikerGegevens(string gebruikernaam)
        {
            return rep.GetGebruikersGegevens(gebruikernaam);
        }
        public List<Gebruiker> GetGebruikersLijst()
        {
            gebruikers = rep.GetGebruikersLijst();
            return gebruikers;
        }
    }
}