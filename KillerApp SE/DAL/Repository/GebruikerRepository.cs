using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerApp_SE.SQLContext;
using KillerApp_SE.Models;

namespace KillerApp_SE.SQLRepository
{
    public class GebruikerRepository
    {
        SQLPersistency db = new SQLPersistency();

        public bool Login(string gebruikernaam, string wachtwoord)
        {
            return db.Login(gebruikernaam, wachtwoord);
        }
        public List<Gebruiker> GetGebruikersLijst()
        {
            return db.GetGebruikersLijst();
        }
        public void GebruikerToevoegen(string gebruikersnaam, string wachtwoord, string naam, string adres, string geboortedatum, string status)
        {
            db.GebruikerToevoegen(gebruikersnaam, wachtwoord, naam, adres, geboortedatum, "Gebruiker");
        }
        public void GebruikerVerwijderen(string gebruikersnaam)
        {
            db.GebruikerVerwijderen(gebruikersnaam);
        }
        public void WijzigGegevens(string gebruikernaam, string naam, string adres, string geboortedatum, string wachtwoord)
        {
            db.WijzigGegevens(gebruikernaam, naam, adres, geboortedatum, wachtwoord);
        }
    }
}