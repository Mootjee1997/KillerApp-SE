using System.Collections.Generic;
using KillerApp_SE.SQLRepository;

namespace KillerApp_SE.Models
{
    public class Bibliotheek
    {
        GebruikerRepository grep = new GebruikerRepository();
        BoekRepository brep = new BoekRepository();
        public List<Gebruiker> gebruikers = new List<Gebruiker>();
        public List<Boek> boeken = new List<Boek>();

        //Zoekt binnen de geleende boekenlijst van een gebruiker naar zijn boeken en geeft aan of de titel erin voorkomt
        public bool ZoekMijnBoekenLijst(string gebruikernaam, string titel)
        {
            foreach (Boek b in GetMijnBoeken(gebruikernaam))
            {
                if (b.Titel == titel)
                {
                    return true;
                }
            }
            return false;
        }
        //Haalt de status van een gebruiker op, mogelijke statussen zijn: "Admin" en "Gebruiker"
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
        //Haalt de naam van een gebruiker op
        public string GetNaam(string gebruikernaam)
        {
            GetGebruikersLijst();
            foreach (Gebruiker g in gebruikers)
            {
                if (g.Gebruikernaam == gebruikernaam)
                {
                    return g.Naam;
                }
            }
            return null;
        }
        //Zoekt naar boeken binnen de boekenlijst die voldoen aan de zoekterm
        public List<Boek> ZoekBoek(string titel)
        {
            return brep.ZoekBoek(titel);
        }
        //Zoekt naar een gebruiker binnen de gebruikerslijst en haalt de model daarvan op
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
        //Verwijdert een gebruiker uit de gebruikerslijst
        public void VerwijderGebruiker(string gebruikernaam)
        {
            GetGebruikersLijst();
            foreach (Gebruiker g in gebruikers)
            {
                if (g.Gebruikernaam == gebruikernaam)
                {
                    grep.GebruikerVerwijderen(gebruikernaam); grep.GetGebruikersLijst();
                }
            }
        }
        //Methode voor het inloggen
        public bool Login(string gebruikernaam, string wachtwoord)
        {
            return grep.Login(gebruikernaam, wachtwoord);
        }
        //Gebruiker toevoegen aan de gebruikerslijst, standaard status: 'Gebruiker'
        public void GebruikerToevoegen(string gebruikernaam, string wachtwoord, string naam, string adres, string geboortedatum)
        {
            grep.GebruikerToevoegen(gebruikernaam, wachtwoord, naam, adres, geboortedatum, "Gebruiker"); grep.GetGebruikersLijst();
        }
        //Gegevens van een gebruiker wijzigen
        public void WijzigGegevens(string gebruikernaam, string naam, string adres, string geboortedatum, string wachtwoord)
        {
            grep.WijzigGegevens(gebruikernaam, naam, adres, geboortedatum, wachtwoord); grep.GetGebruikersLijst();
        }
        //Methode voor het lenen van een boek door een gebruiker, lenen is maar 1 x mogelijk per boek
        public void LeenBoek(string gebruikernaam, string titel)
        {
            GetBoekenLijst();
            foreach (Boek boek in boeken)
            {
                if (boek.Titel == titel && boek.AantalBeschikbaar > 0)
                {
                    brep.LeenBoek(gebruikernaam, titel); brep.UpdateBoek(titel);
                }
            }
        }
        //Boek retourneren
        public void RetourBoek(string gebruikernaam, string titel)
        {
            brep.RetourBoek(gebruikernaam, titel);
        }
        //Haalt de lijst van gebruikers op uit de database
        public List<Gebruiker> GetGebruikersLijst()
        {
            return gebruikers = grep.GetGebruikersLijst();
        }
        //Haalt de lijst van boeken op uit de database
        public List<Boek> GetBoekenLijst()
        {
            return boeken = brep.GetBoekenlijst();
        }
        //Haalt de lijst met boeken op die op een specifieke gebruiker zijn naam staan
        public List<Boek> GetMijnBoeken(string gebruikernaam)
        {
            return brep.GetMijnBoeken(gebruikernaam);
        }
    }
}