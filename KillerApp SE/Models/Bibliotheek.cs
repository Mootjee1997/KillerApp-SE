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
        GebruikerRepository grep = new GebruikerRepository();
        BoekRepository brep = new BoekRepository();
        public List<Gebruiker> gebruikers = new List<Gebruiker>();
        public List<Boek> boeken = new List<Boek>();

        public string GetStatus(string gebruikernaam)
        {
            GetGebruikersLijst();
            foreach (Gebruiker g in gebruikers)
            {
                if (g.Gebruikernaam == gebruikernaam)
                {
                    return g.Status;
                }
            }
            return null;
        }
        public List<Boek> ZoekBoek(string titel)
        {
            return brep.ZoekBoek(titel);
        }
        public Gebruiker Zoekgebruiker(string gebruikernaam)
        {
            GetGebruikersLijst();
            foreach (Gebruiker g in gebruikers)
            {
                if (g.Gebruikernaam == gebruikernaam)
                {
                    return g;
                }
            }
            return null;
        }
        public void VerwijderGebruiker(string gebruikernaam)
        {
            GetGebruikersLijst();
            foreach (Gebruiker g in gebruikers)
            {
                if (g.Gebruikernaam == gebruikernaam)
                {
                    grep.GebruikerVerwijderen(gebruikernaam);
                }
            }
            grep.GetGebruikersLijst();
        }
        public bool Login(string gebruikernaam, string wachtwoord)
        {
            return grep.Login(gebruikernaam, wachtwoord);
        }
        //Gebruiker toevoegen
        public void GebruikerToevoegen(string gebruikernaam, string wachtwoord, string naam, string adres, string geboortedatum)
        {
            grep.GebruikerToevoegen(gebruikernaam, wachtwoord, naam, adres, geboortedatum, "Gebruiker");
            grep.GetGebruikersLijst();
        }
        //Gegevens van een gebruiker wijzigen
        public void WijzigGegevens(string gebruikernaam, string naam, string adres, string geboortedatum, string wachtwoord)
        {
            grep.WijzigGegevens(gebruikernaam, naam, adres, geboortedatum, wachtwoord);
            grep.GetGebruikersLijst();
        }
        public List<Gebruiker> GetGebruikersLijst()
        {
            gebruikers = grep.GetGebruikersLijst();
            return gebruikers;
        }
        public List<Boek> GetBoekenLijst()
        {
            boeken = brep.GetBoekenlijst();
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
                        brep.LeenBoek(gebruikernaam, titel);
                        brep.UpdateBoek(titel);
                    }
                }
            }
        }
        public void RetourBoek(string gebruikernaam, string titel)
        {
            brep.RetourBoek(gebruikernaam, titel);
        }
        public List<Boek> GetMijnBoeken(string gebruikernaam)
        {
            return brep.GetMijnBoeken(gebruikernaam);
        }
    }
}