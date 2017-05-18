using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerApp_SE.SQLContext;
using KillerApp_SE.Models;

namespace KillerApp_SE.SQLRepository
{
    public class Repository
    {
        SQLPersistency db = new SQLPersistency();

        public List<string> Login(string gebruikernaam, string wachtwoord)
        {
            return db.Login(gebruikernaam, wachtwoord);
        }
        public List<string> GetBoekenlijst()
        {
            return db.GetBoekenlijst();
        }
        public List<string> GetGebruikersGegevens(string gebruikernaam)
        {
            return db.GetGebruikersGegevens(gebruikernaam);
        }
        public List<Gebruiker> GetGebruikersLijst()
        {
            return db.GetGebruikersLijst();
        }
        public void GebruikerToevoegen(string gebruikersnaam, string wachtwoord, string naam, string adres, string geboortedatum)
        {
            db.GebruikerToevoegen(gebruikersnaam, wachtwoord, naam, adres, geboortedatum);
        }
        public void GebruikerVerwijderen(string gebruikersnaam)
        {
            db.GebruikerVerwijderen(gebruikersnaam);
        }
        public void GebruikerResetten(string gebruikersnaam, string wachtwoord)
        {
            db.GebruikerResetten(gebruikersnaam, wachtwoord);
        }
        public void LeenBoek(string gebruikernaam, string titel)
        {
            db.LeenBoek(gebruikernaam, titel);
        }
        public void ReturnBoeken(string gebruikernaam)
        {
            db.ReturnBoeken(gebruikernaam);
        }
        public List<string> GetBoekInfo(string titel)
        {
            return db.GetBoekInfo(titel);
        }
        public void WijzigGegevens(string gebruikernaam, string naam, string adres, string geboortedatum, string wachtwoord)
        {
            db.WijzigGegevens(gebruikernaam, naam, adres, geboortedatum, wachtwoord);
        }
    }
}