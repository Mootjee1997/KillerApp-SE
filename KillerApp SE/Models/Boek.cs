using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp_SE.Models
{
    public class Boek
    {
        private Auteur auteur;
        private Uitgever uitgever;
        private string titel;
        private string genre;
        private int aantalBeschikbaar;
        private int aantalExemplaren;

        public Auteur Auteur
        {
            get { return auteur; }
            set { auteur = value; }
        }
        public Uitgever Uitgever
        {
            get { return uitgever; }
            set { uitgever = value; }
        }
        public string Titel
        {
            get { return titel; }
            set { titel = value; }
        }
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }
        public int AantalExemplaren
        {
            get { return aantalExemplaren; }
            set { aantalExemplaren = value; }
        }
        public int AantalBeschikbaar
        {
            get { return aantalBeschikbaar; }
            set { aantalBeschikbaar = value; }
        }

        public Boek(Auteur auteur, Uitgever uitgever, string titel, string genre, int aantalBeschikbaar, int aantalExemplaren)
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