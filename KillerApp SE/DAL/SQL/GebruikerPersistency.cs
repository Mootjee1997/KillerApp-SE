using System.Data.SqlClient;
using System.Collections.Generic;
using KillerApp_SE.Models;
using KillerApp_SE.DAL;
using System;

namespace KillerApp_SE.SQLContext
{
    public class GebruikerPersistency
    {
        string query;

        public bool Login(string gebruikernaam, string wachtwoord)
        {
            try
            {
                Database.CheckConn();
                query = "SELECT * FROM Gebruiker WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE (Username = @Gebruikernaam) AND (Password = @Wachtwoord))";
                SqlCommand cmd = new SqlCommand(query, Database.conn);
                cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
                cmd.Parameters.AddWithValue("@Wachtwoord", wachtwoord);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
                return false;
            }
        }
        public List<Gebruiker> GetGebruikersLijst()
        {
            try
            {
                List<Gebruiker> gebruikers = new List<Gebruiker>();
                Database.CheckConn();
                query = "SELECT Username, Password, Naam, Adres, Geboortedatum, Status FROM Gebruiker INNER JOIN Login ON Gebruiker.GebruikerID = Login.GebruikerID";
                SqlCommand cmd = new SqlCommand(query, Database.conn);
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
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
                return null;
            }

        }
        public void GebruikerToevoegen(Gebruiker gebruiker)
        {
            try
            {
                Database.CheckConn();
                query = "INSERT INTO Gebruiker (Naam, Adres, Geboortedatum) VALUES (@Naam, @Adres, @Geboortedatum)";
                SqlCommand cmd = new SqlCommand(query, Database.conn);
                cmd.Parameters.AddWithValue("@Naam", gebruiker.Naam);
                cmd.Parameters.AddWithValue("@Adres", gebruiker.Adres);
                cmd.Parameters.AddWithValue("@Geboortedatum", gebruiker.Geboortedatum);
                cmd.ExecuteNonQuery();

                Database.CheckConn();
                query = "INSERT INTO Login (GebruikerID, Username, Password, Status) VALUES ((SELECT GebruikerID FROM Gebruiker WHERE Naam = @Naam AND Adres = @Adres), @Gebruikersnaam, @Wachtwoord, @Status)";
                SqlCommand cmd2 = new SqlCommand(query, Database.conn);
                cmd2.Parameters.AddWithValue("@Naam", gebruiker.Naam);
                cmd2.Parameters.AddWithValue("@Adres", gebruiker.Adres);
                cmd2.Parameters.AddWithValue("@Gebruikersnaam", gebruiker.Gebruikernaam);
                cmd2.Parameters.AddWithValue("@Wachtwoord", gebruiker.Wachtwoord);
                cmd2.Parameters.AddWithValue("@Status", gebruiker.Status);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
            }
        }
        public void GebruikerVerwijderen(string gebruikernaam)
        {
            try
            {
                Database.CheckConn();
                query = "IF(SELECT COUNT(*) FROM [Boek-Gebruiker] WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam)) = 0 DELETE FROM Gebruiker WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam); DELETE FROM Login WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam)";
                SqlCommand cmd = new SqlCommand(query, Database.conn);
                cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
            }
        }
        public void WijzigGegevens(Gebruiker gebruiker)
        {
            try
            {
                Database.CheckConn();
                query = "UPDATE GEBRUIKER SET Naam = @Naam, Adres = @Adres, Geboortedatum = @Geboortedatum WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam)";
                SqlCommand cmd = new SqlCommand(query, Database.conn);
                cmd.Parameters.AddWithValue("@Naam", gebruiker.Naam);
                cmd.Parameters.AddWithValue("@Adres", gebruiker.Adres);
                cmd.Parameters.AddWithValue("@Geboortedatum", gebruiker.Geboortedatum);
                cmd.Parameters.AddWithValue("@Gebruikernaam", gebruiker.Gebruikernaam);
                cmd.ExecuteNonQuery();

                Database.CheckConn();
                query = "UPDATE Login SET Password = @Wachtwoord WHERE Username = @Gebruikernaam";
                SqlCommand cmd2 = new SqlCommand(query, Database.conn);
                cmd2.Parameters.AddWithValue("@Gebruikernaam", gebruiker.Gebruikernaam);
                cmd2.Parameters.AddWithValue("@Wachtwoord", gebruiker.Wachtwoord);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
            }
        }
    }
}