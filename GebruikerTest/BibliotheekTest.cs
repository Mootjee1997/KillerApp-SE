using Microsoft.VisualStudio.TestTools.UnitTesting;
using KillerApp_SE.Models;

namespace UnitTests
{
    [TestClass]
    public class BibliotheekTest
    {
        [TestMethod]
        public void LeenBoek()
        {
            //Arrange
            Auteur auteur = new Auteur("Bert", "Berenstraat 39");
            Uitgever uitgever = new Uitgever("Dirk", "Leeuwenstraat 93");
            Boek boek = new Boek(auteur, uitgever, "Suske & Wiske", "Horror", 5, 5);
            Gebruiker gebruiker = new Gebruiker("Admin", "Admin", "Mohamed Ali", "Vlinderstraat 76", "29-09-1997", "Admin");
            //Act
            gebruiker.LeenBoek(boek);
            //Assert
            Assert.IsTrue(gebruiker.Boeken.Contains(boek));
        }
        [TestMethod]
        public void RetourBoek()
        {
            //Arrange
            Auteur auteur = new Auteur("Bert", "Berenstraat 39");
            Uitgever uitgever = new Uitgever("Dirk", "Leeuwenstraat 93");
            Boek boek = new Boek(auteur, uitgever, "Suske & Wiske", "Horror", 5, 5);
            Gebruiker gebruiker = new Gebruiker("Admin", "Admin", "Mohamed Ali", "Vlinderstraat 76", "29-09-1997", "Admin");
            //Act
            gebruiker.Boeken.Add(boek);
            gebruiker.RetourBoek(boek);
            //Assert
            Assert.IsTrue(!gebruiker.Boeken.Contains(boek));
        }
    }
}
