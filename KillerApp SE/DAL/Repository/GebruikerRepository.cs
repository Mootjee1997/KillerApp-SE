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
        public void GebruikerToevoegen(Gebruiker gebruiker)
        {
            gpers.GebruikerToevoegen(gebruiker);
        }
        public void GebruikerVerwijderen(string gebruikersnaam)
        {
            gpers.GebruikerVerwijderen(gebruikersnaam);
        }
        public void WijzigGegevens(Gebruiker gebruiker)
        {
            gpers.WijzigGegevens(gebruiker);
        }
    }
}