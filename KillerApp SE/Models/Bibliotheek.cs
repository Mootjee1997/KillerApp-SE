using System.Collections.Generic;
using KillerApp_SE.SQLRepository;

namespace KillerApp_SE.Models
{
    public static class Bibliotheek
    {
        //Zoekt naar boeken binnen de boekenlijst die voldoen aan de gegeven zoekterm
        public static List<Boek> ZoekBoek(string titel)
        {
            return brep.ZoekBoek(titel);
        }
        //Zoekt binnen de boekenlijst van een gebruiker naar een bepaalde titel
        public static bool CheckMijnLijst(string gebruikernaam, string titel)
        {
            foreach (Boek b in GetGebruiker(gebruikernaam).Boeken) if (b.Titel == titel) return true;
            return false;
        }
        //Zoekt naar een gebruiker binnen de gebruikerslijst en haalt de model daarvan op
        public static Gebruiker GetGebruiker(string gebruikernaam)
        {
            foreach (Gebruiker g in gebruikers) if (g.Gebruikernaam == gebruikernaam) return g;
            return null;
        }
        //Zoekt naar een boek binnen de boekenlijst en haalt de model daarvan op
        public static Boek GetBoek(string titel)
        {
            foreach (Boek b in boeken) if (b.Titel == titel) return b;
            return null;
        }
        //Methode voor het inloggen
        public static bool Login(string gebruikernaam, string wachtwoord)
        {
            return grep.Login(gebruikernaam, wachtwoord);
        }
        //Gebruiker toevoegen aan de gebruikerslijst, standaard status: 'Gebruiker'
        public static void GebruikerToevoegen(Gebruiker gebruiker)
        {
            gebruikers.Add(gebruiker);
            grep.GebruikerToevoegen(gebruiker);
        }
        //Verwijdert een gebruiker uit de gebruikerslijst
        public static void VerwijderGebruiker(string gebruikernaam)
        {
            gebruikers.Remove(GetGebruiker(gebruikernaam));
            grep.GebruikerVerwijderen(gebruikernaam);
        }
        //Gegevens van een gebruiker wijzigen
        public static void WijzigGegevens(string gebruikernaam, string naam, string adres, string geboortedatum, string wachtwoord)
        {
            Gebruiker gebruiker = GetGebruiker(gebruikernaam);
            gebruiker.Naam = naam;
            gebruiker.Adres = adres;
            gebruiker.Geboortedatum = geboortedatum;
            gebruiker.Wachtwoord = wachtwoord;
            grep.WijzigGegevens(gebruiker);
        }
        //Haalt de lijst van gebruikers en bijbeborende boeken op uit de database
        public static void GetGebruikersLijst()
        {
            gebruikers = grep.GetGebruikersLijst();
            foreach (Gebruiker g in gebruikers) g.Boeken = brep.GetMijnBoeken(g.Gebruikernaam);
        }
        //Haalt de lijst van boeken op uit de database
        public static void GetBoekenLijst()
        {
            boeken = brep.GetBoekenlijst();
        }
        public static List<Gebruiker> Gebruikers { get { return gebruikers; } set { gebruikers = value; } }
        public static List<Boek> Boeken { get { return boeken; } set { boeken = value; } }
        private static List<Gebruiker> gebruikers = new List<Gebruiker>();
        private static List<Boek> boeken = new List<Boek>();
        public static GebruikerRepository grep = new GebruikerRepository();
        public static BoekRepository brep = new BoekRepository();
    }
}