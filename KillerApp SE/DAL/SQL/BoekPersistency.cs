using System.Data.SqlClient;
using System.Collections.Generic;
using KillerApp_SE.Models;
using KillerApp_SE.DAL;
using System;

namespace KillerApp_SE.SQLContext
{
    public class BoekPersistency
    {
        string query;

        public List<Boek> ZoekBoek(string titel)
        {
            try
            {
                List<Boek> boeken = new List<Boek>();
                Database.CheckConn();
                query = "SELECT AuteurNaam, AuteurAdres, UitgeverNaam, UitgeverAdres, Titel, Genre, AantalExemplaren, AantalBeschikbaar FROM Boek INNER JOIN Auteur ON Auteur.AuteurID = Boek.AuteurID INNER JOIN Uitgever ON Uitgever.UitgeverID = Boek.UitgeverID WHERE Titel LIKE '%' + @Titel + '%'";
                SqlCommand cmd = new SqlCommand(query, Database.conn);
                cmd.Parameters.AddWithValue("@Titel", titel);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Uitgever uitgever = new Uitgever(reader.GetString(2), reader.GetString(3));
                        Auteur auteur = new Auteur(reader.GetString(0), reader.GetString(1));
                        Boek boek = new Boek(auteur, uitgever, reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7));
                        boeken.Add(boek);
                    }
                }
                return boeken;
            }
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
                return null;
            }
        }
        public List<Boek> GetBoekenlijst()
        {
            try
            {
                List<Boek> boeken = new List<Boek>();
                Database.CheckConn();
                query = "SELECT AuteurNaam, AuteurAdres, UitgeverNaam, UitgeverAdres, Titel, Genre, AantalBeschikbaar, AantalExemplaren FROM Boek INNER JOIN Auteur ON Auteur.AuteurID = Boek.AuteurID INNER JOIN Uitgever ON Uitgever.UitgeverID = Boek.UitgeverID";
                SqlCommand cmd = new SqlCommand(query, Database.conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Uitgever uitgever = new Uitgever(reader.GetString(2), reader.GetString(3));
                        Auteur auteur = new Auteur(reader.GetString(0), reader.GetString(1));
                        Boek boek = new Boek(auteur, uitgever, reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7));
                        boeken.Add(boek);
                    }
                }
                return boeken;
            }
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
                return null;
            }
        }
        public List<Boek> GetMijnBoeken(string gebruikernaam)
        {
            try
            {
                List<Boek> boeken = new List<Boek>();
                Database.CheckConn();
                query = "SELECT AuteurNaam, AuteurAdres, UitgeverNaam, UitgeverAdres, Titel, Genre, AantalExemplaren, AantalBeschikbaar FROM Boek INNER JOIN Auteur ON Auteur.AuteurID = Boek.AuteurID INNER JOIN Uitgever ON Uitgever.UitgeverID = Boek.UitgeverID WHERE BoekID IN (SELECT BoekID FROM [Boek-Gebruiker] WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam))";
                SqlCommand cmd = new SqlCommand(query, Database.conn);
                cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Uitgever uitgever = new Uitgever(reader.GetString(2), reader.GetString(3));
                        Auteur auteur = new Auteur(reader.GetString(0), reader.GetString(1));
                        Boek boek = new Boek(auteur, uitgever, reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7));
                        boeken.Add(boek);
                    }
                }
                return boeken;
            }
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
                return null;
            }
        }
        public void UpdateBoek(Boek boek)
        {
            try
            {
                Database.CheckConn();
                query = "UPDATE Boek SET AantalBeschikbaar = @AantalBeschikbaar WHERE Titel = @Titel";
                SqlCommand cmd = new SqlCommand(query, Database.conn);
                cmd.Parameters.AddWithValue("@Titel", boek.Titel);
                cmd.Parameters.AddWithValue("@AantalBeschikbaar", boek.AantalBeschikbaar);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
            }
        }
        public void LeenBoek(string gebruikernaam, Boek boek)
        {
            try
            {
                Database.CheckConn();
                query = "INSERT INTO [Boek-Gebruiker] (BoekID, GebruikerID) VALUES ((SELECT BoekID FROM Boek WHERE Titel = @Titel), (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam))";
                SqlCommand cmd = new SqlCommand(query, Database.conn);
                cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
                cmd.Parameters.AddWithValue("@Titel", boek.Titel);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
            }
        }
        public void RetourBoek(string gebruikernaam, Boek boek)
        {
            try { 
            Database.CheckConn();
            query = "DELETE FROM [Boek-Gebruiker] WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam) AND BoekID = (SELECT BoekID FROM Boek WHERE Titel = @Titel)";
            SqlCommand cmd = new SqlCommand(query, Database.conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.Parameters.AddWithValue("@Titel", boek.Titel);
            cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Database.exceptionMessage = e.ToString();
                Database.conn.Close();
            }
        }
    }
}