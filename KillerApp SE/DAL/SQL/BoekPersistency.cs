using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using KillerApp_SE.Models;

namespace KillerApp_SE.SQLContext
{
    public class BoekPersistency
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-JKI4SS5;Initial Catalog=FUN2;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);
        string query;

        public void CheckConn()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }
        public List<Boek> ZoekBoek(string titel)
        {
            List<Boek> boeken = new List<Boek>();
            CheckConn();
            query = "SELECT AuteurNaam, UitgeverNaam, Titel, Genre, AantalExemplaren, AantalBeschikbaar FROM Boek INNER JOIN Auteur ON Auteur.AuteurID = Boek.AuteurID INNER JOIN Uitgever ON Uitgever.UitgeverID = Boek.UitgeverID WHERE Titel LIKE '%' + @Titel + '%'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Titel", titel);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Boek boek = new Boek(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(5), reader.GetInt32(4));
                    boeken.Add(boek);
                }
            }
            return boeken;
        }
        public List<Boek> GetBoekenlijst()
        {
            List<Boek> boeken = new List<Boek>();
            CheckConn();
            query = "SELECT AuteurNaam, UitgeverNaam, Titel, Genre, AantalExemplaren, AantalBeschikbaar FROM Boek INNER JOIN Auteur ON Auteur.AuteurID = Boek.AuteurID INNER JOIN Uitgever ON Uitgever.UitgeverID = Boek.UitgeverID";
            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Boek boek = new Boek(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(5), reader.GetInt32(4));
                    boeken.Add(boek);
                }
            }
            return boeken;
        }
        public List<Boek> GetMijnBoeken(string gebruikernaam)
        {
            List<Boek> boeken = new List<Boek>();
            CheckConn();
            query = "SELECT AuteurNaam, UitgeverNaam, Titel, Genre, AantalExemplaren, AantalBeschikbaar FROM Boek INNER JOIN Auteur ON Auteur.AuteurID = Boek.AuteurID INNER JOIN Uitgever ON Uitgever.UitgeverID = Boek.UitgeverID WHERE BoekID IN (SELECT BoekID FROM [Boek-Gebruiker] WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam))";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Boek boek = new Boek(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(5), reader.GetInt32(4));
                    boeken.Add(boek);
                }
            }
            return boeken;
        }
        public List<Gebruiker> GetGebruikersLijst()
        {
            List<Gebruiker> gebruikers = new List<Gebruiker>();
            CheckConn();
            query = "SELECT Username, Password, Naam, Adres, Geboortedatum, Status FROM Gebruiker INNER JOIN Login ON Gebruiker.GebruikerID = Login.GebruikerID";
            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Gebruiker gebruiker = new Gebruiker(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    gebruikers.Add(gebruiker);
                }
            }
            return gebruikers;
        }
        public void UpdateBoek(string titel)
        {
            CheckConn();
            query = "UPDATE Boek SET AantalBeschikbaar = AantalBeschikbaar - 1 WHERE Titel = @Titel";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Titel", titel);
            cmd.ExecuteNonQuery();
        }
        public void LeenBoek(string gebruikernaam, string titel)
        {
            CheckConn();
            query = "INSERT INTO [Boek-Gebruiker] (BoekID, GebruikerID) VALUES ((SELECT BoekID FROM Boek WHERE Titel = @Titel), (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam))";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.Parameters.AddWithValue("@Titel", titel);
            cmd.ExecuteNonQuery();
        }
        public void RetourBoek(string gebruikernaam, string titel)
        {
            CheckConn();
            query = "DELETE FROM [Boek-Gebruiker] WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam) AND BoekID = (SELECT BoekID FROM Boek WHERE Titel = @Titel)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.Parameters.AddWithValue("@Titel", titel);
            cmd.ExecuteNonQuery();

            CheckConn();
            query = "UPDATE Boek SET AantalBeschikbaar = AantalBeschikbaar + 1 WHERE Titel = @Titel";
            SqlCommand cmd2 = new SqlCommand(query, conn);
            cmd2.Parameters.AddWithValue("@Titel", titel);
            cmd2.ExecuteNonQuery();
        }
    }
}