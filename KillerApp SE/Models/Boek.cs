using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp_SE.Models
{
    public class Boek
    {
        private string auteur;
        private string uitgever;
        private string titel;
        private string genre;
        private int aantalBeschikbaar;
        private int aantalExemplaren;
        
        public string Auteur
        {
            get { return auteur; } set { auteur = value; }
        }
        public string Uitgever
        {
            get { return uitgever; } set { uitgever = value; }
        }
        public string Titel
        {
            get { return titel; } set { titel = value; }
        }
        public string Genre
        {
            get { return genre; } set { genre = value; }
        }
        public int AantalExemplaren
        {
            get { return aantalExemplaren; } set { aantalExemplaren = value; }
        }
        public int AantalBeschikbaar
        {
            get { return aantalBeschikbaar; } set { aantalBeschikbaar = value; }
        }

        public Boek(string auteur, string uitgever, string titel, string genre, int aantalBeschikbaar, int aantalExemplaren)
        {
            this.auteur = auteur;
            this.uitgever = uitgever;
            this.titel = titel;
            this.genre = genre;
            this.aantalBeschikbaar = aantalBeschikbaar;
            this.aantalExemplaren = aantalExemplaren;
        }

        public override string ToString()
        {
            return this.titel;
        }
    }
}