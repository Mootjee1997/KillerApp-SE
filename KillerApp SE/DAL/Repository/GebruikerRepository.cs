using System.Collections.Generic;
using KillerApp_SE.SQLContext;
using KillerApp_SE.Models;

namespace KillerApp_SE.SQLRepository
{
    public class GebruikerRepository
    {
        GebruikerPersistency gpers = new GebruikerPersistency();

        public bool Login(string gebruikernaam, string wachtwoord)
        {
            return gpers.Login(gebruikernaam, wachtwoord);
        }
        public List<Gebruiker> GetGebruikersLijst()
        {
            return gpers.GetGebruikersLijst();
        }
        public void GebruikerToevoegen(string gebruikersnaam, string wachtwoord, string naam, string adres, string geboortedatum, string status)
        {
            gpers.GebruikerToevoegen(gebruikersnaam, wachtwoord, naam, adres, geboortedatum, "Gebruiker");
        }
        public void GebruikerVerwijderen(string gebruikersnaam)
        {
            gpers.GebruikerVerwijderen(gebruikersnaam);
        }
        public void WijzigGegevens(string gebruikernaam, string naam, string adres, string geboortedatum, string wachtwoord)
        {
            gpers.WijzigGegevens(gebruikernaam, naam, adres, geboortedatum, wachtwoord);
        }
    }
}