using Microsoft.VisualStudio.TestTools.UnitTesting;
using KillerApp_SE.Models;

namespace UnitTests
{
    [TestClass]
    public class BibliotheekTest
    {
        [TestMethod]
        public void CheckMijnLijst()
        {
            //Arrange
            Auteur auteur = new Auteur("Bert", "Berenstraat 39");
            Uitgever uitgever = new Uitgever("Dirk", "Leeuwenstraat 93");
            Boek boek = new Boek(auteur, uitgever, "Suske & Wiske", "Horror", 5, 5);
            Gebruiker gebruiker = new Gebruiker("Admin", "Admin", "Mohamed Ali", "Vlinderstraat 76", "29-09-1997", "Admin");
            //Act
            gebruiker.Boeken.Add(boek);
            //Assert
            Assert.IsTrue(Bibliotheek.CheckMijnLijst("Admin", boek.Titel));
        }
        [TestMethod]
        public void GetGebruiker()
        {
            //Arrange
            Gebruiker gebruiker = new Gebruiker("Admin", "Admin", "Mohamed Ali", "Vlinderstraat 76", "29-09-1997", "Admin");
            //Assert
            Assert.AreEqual(Bibliotheek.GetGebruiker(gebruiker.Gebruikernaam), gebruiker);
        }
        [TestMethod]
        public void GetBoek()
        {
            //Arrange
            Auteur auteur = new Auteur("Bert", "Berenstraat 39");
            Uitgever uitgever = new Uitgever("Dirk", "Leeuwenstraat 93");
            Boek boek = new Boek(auteur, uitgever, "Suske & Wiske", "Horror", 5, 5);
            //Assert
            Assert.AreEqual(Bibliotheek.GetBoek(boek.Titel), boek);
        }
        [TestMethod]
        public void GebruikerToevoegen()
        {
            //Arrange
            Gebruiker gebruiker = new Gebruiker("Admin", "Admin", "Mohamed Ali", "Vlinderstraat 76", "29-09-1997", "Admin");
            //Act
            Bibliotheek.GebruikerToevoegen(gebruiker);
            //Assert
            Assert.IsTrue(Bibliotheek.Gebruikers.Contains(gebruiker));
        }
        [TestMethod]
        public void GebruikerVerwijderen()
        {
            //Arrange
            Gebruiker gebruiker = new Gebruiker("Admin", "Admin", "Mohamed Ali", "Vlinderstraat 76", "29-09-1997", "Admin");
            //Act
            Bibliotheek.GebruikerToevoegen(gebruiker);
            Bibliotheek.VerwijderGebruiker(gebruiker.Gebruikernaam);
            //Assert
            Assert.IsFalse(Bibliotheek.Gebruikers.Contains(gebruiker));
        }
        [TestMethod]
        public void WijzigGegevens()
        {
            //Arrange
            Gebruiker gebruiker = new Gebruiker("Admin", "Admin", "Mohamed Ali", "Vlinderstraat 76", "29-09-1997", "Admin");
            //Act
            Bibliotheek.GebruikerToevoegen(gebruiker);
            Bibliotheek.WijzigGegevens(gebruiker.Gebruikernaam, "Naam", "Adres", "Geboortedatum", "Wachtwoord");
            //Assert
            Assert.IsTrue(Bibliotheek.GetGebruiker(gebruiker.Gebruikernaam).Naam == "Naam");
        }
        [TestMethod]
        public void GetBoekenLijst()
        {
            //Act
            Bibliotheek.GetBoekenLijst();
            //Assert
            Assert.IsTrue(Bibliotheek.Boeken.Count > 0);
        }
        [TestMethod]
        public void GetGebruikersLijst()
        {
            //Act
            Bibliotheek.GetGebruikersLijst();
            //Assert
            Assert.IsTrue(Bibliotheek.Gebruikers.Count > 0);
        }
    }
}
