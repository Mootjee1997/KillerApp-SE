﻿using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using KillerApp_SE.Models;

namespace KillerApp_SE.SQLContext
{
    public class GebruikerPersistency
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
        public bool Login(string gebruikernaam, string wachtwoord)
        {
            CheckConn();
            query = "SELECT * FROM Gebruiker WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE (Username = @Gebruikernaam) AND (Password = @Wachtwoord))";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.Parameters.AddWithValue("@Wachtwoord", wachtwoord);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return true;
                    }
                }
            }
            return false;
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
        public void GebruikerToevoegen(string gebruikersnaam, string wachtwoord, string naam, string adres, string geboortedatum, string status)
        {
            CheckConn();
            query = "INSERT INTO Gebruiker (Naam, Adres, Geboortedatum) VALUES (@Naam, @Adres, @Geboortedatum)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Naam", naam);
            cmd.Parameters.AddWithValue("@Adres", adres);
            cmd.Parameters.AddWithValue("@Geboortedatum", geboortedatum);
            cmd.ExecuteNonQuery();

            CheckConn();
            query = "INSERT INTO Login (GebruikerID, Username, Password, Status) VALUES ((SELECT GebruikerID FROM Gebruiker WHERE Naam = @Naam AND Adres = @Adres), @Gebruikersnaam, @Wachtwoord, @Status)";
            SqlCommand cmd2 = new SqlCommand(query, conn);
            cmd2.Parameters.AddWithValue("@Naam", naam);
            cmd2.Parameters.AddWithValue("@Adres", adres);
            cmd2.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
            cmd2.Parameters.AddWithValue("@Wachtwoord", wachtwoord);
            cmd2.Parameters.AddWithValue("@Status", status);
            cmd2.ExecuteNonQuery();
        }
        public void GebruikerVerwijderen(string gebruikernaam)
        {
            CheckConn();
            query = "IF(SELECT COUNT(*) FROM [Boek-Gebruiker] WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam)) = 0 DELETE FROM Gebruiker WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam); DELETE FROM Login WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.ExecuteNonQuery();
        }
        public void WijzigGegevens(string gebruikernaam, string naam, string adres, string geboortedatum, string wachtwoord)
        {
            CheckConn();
            query = "UPDATE GEBRUIKER SET Naam = @Naam, Adres = @Adres, Geboortedatum = @Geboortedatum WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Naam", naam);
            cmd.Parameters.AddWithValue("@Adres", adres);
            cmd.Parameters.AddWithValue("@Geboortedatum", geboortedatum);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.ExecuteNonQuery();

            CheckConn();
            query = "UPDATE Login SET Password = @Wachtwoord WHERE Username = @Gebruikernaam";
            SqlCommand cmd2 = new SqlCommand(query, conn);
            cmd2.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd2.Parameters.AddWithValue("@Wachtwoord", wachtwoord);
            cmd2.ExecuteNonQuery();
        }
    }
}