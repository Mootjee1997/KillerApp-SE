using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp_SE.Models
{
    public class Boek
    {
        private string titel;
        private string genre;
        private int aantalBeschikbaar;
        private int aantalExemplaren;

        public Boek(string titel, string genre, int aantalBeschikbaar, int aantalExemplaren)
        {
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