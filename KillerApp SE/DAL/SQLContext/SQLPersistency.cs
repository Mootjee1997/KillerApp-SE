using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace KillerApp_SE.SQLContext
{
    public class SQLPersistency
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-JKI4SS5;Initial Catalog=FUN2;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);
        List<string> listResultaat = new List<string>();
        string query;
        string resultaat;

        public void CheckConn()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }
        public List<string> Login(string gebruikernaam, string wachtwoord)
        {
            CheckConn();
            query = "SELECT Naam, Adres, Geboortedatum FROM Gebruiker WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE (Username = @Gebruikernaam) AND (Wachtwoord = @Wachtwoord))";
            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listResultaat.Add(reader.GetString(0));
                    }
                }
            }
            return listResultaat;
        }
        public List<string> GetBoekenlijst()
        {
            CheckConn();
            query = "SELECT Titel FROM Boek";
            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    listResultaat.Add(reader.GetString(0));
                }
            }
            return listResultaat;
        }
        public List<string> GetGebruikerslijst()
        {
            CheckConn();
            query = "SELECT Username FROM Login";
            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    listResultaat.Add(reader.GetString(0));
                }
            }
            return listResultaat;
        }
        public List<string> GetGebruikersGegevens(string gebruikernaam)
        {
            CheckConn();
            query = "SELECT Naam, Adres, Geboortedatum FROM Gebruiker WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    listResultaat.Add(reader.GetString(0));
                    listResultaat.Add(reader.GetString(1));
                    listResultaat.Add(reader.GetString(2));
                }
            }
            return listResultaat;
        }
        public void GebruikerToevoegen(string gebruikersnaam, string wachtwoord, string naam, string adres, string geboortedatum)
        {
            CheckConn();
            query = "INSERT INTO Gebruiker (Naam, Adres, Geboortedatum) VALUES (@Naam, Adres, Geboortedatum)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Naam", naam);
            cmd.Parameters.AddWithValue("@Adres", adres);
            cmd.Parameters.AddWithValue("@Geboortedatum", geboortedatum);
            cmd.ExecuteNonQuery();

            CheckConn();
            query = "INSERT INTO Login (GebruikerID, Username, Password) VALUES ((SELECT GebruikerID FROM Gebruiker WHERE Naam = @Naam AND Adres = @Adres), @Gebruikersnaam, @Wachtwoord)";
            SqlCommand cmd2 = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Naam", naam);
            cmd.Parameters.AddWithValue("@Adres", adres);
            cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
            cmd.Parameters.AddWithValue("@Wachtwoord", wachtwoord);
            cmd.ExecuteNonQuery();
        }
        public void GebruikerVerwijderen(string gebruikernaam)
        {
            CheckConn();
            query = "(IF(SELECT COUNT(*) FROM Boek-Gebruiker WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam)) = 0) DELETE FROM Gebruiker, Login WHERE GebruikerID = @GebruikerID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.ExecuteNonQuery();
        }
        public void GebruikerResetten(string gebruikernaam, string wachtwoord)
        {
            CheckConn();
            query = "UPDATE Login SET Password = @Wachtwoord WHERE Gebruikernaam = @Gebruikernaam";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.Parameters.AddWithValue("@Wachtwoord", wachtwoord);
            cmd.ExecuteNonQuery();
        }
        public void LeenBoek(string gebruikernaam, string titel)
        {
            CheckConn();
            query = "INSERT INTO Boek-Gebruiker (BoekID, GebruikerID) VALUES ((SELECT BoekID FROM Boek WHERE Titel = @Titel), (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam))";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.Parameters.AddWithValue("@Titel", titel);
            cmd.ExecuteNonQuery();
        }
        public void ReturnBoeken(string gebruikernaam)
        {
            CheckConn();
            query = "DELETE FROM Boek-Gebruiker WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.ExecuteNonQuery();
        }
        public List<string> GetBoekInfo(string titel)
        {
            CheckConn();
            query = "SELECT (SELECT Naam FROM Uitgever WHERE UitgeverID = (SELECT UitgeverID FROM Boek WHERE Titel = @Titel)), (SELECT Naam FROM Auteur WHERE AuteurID = (SELECT AuteurID FROM Boek WHERE Titel = @Titel)), Titel, Genre, AantalExemplaren, AantalBeschikbaar FROM Boek WHERE Titel = @Titel";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Titel", titel);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    listResultaat.Add(reader.GetString(0));
                }
            }
            return listResultaat;
        }
        public void WijzigMijnGegevens(string gebruikernaam, string naam, string adres, string geboortedatum)
        {
            CheckConn();
            query = "UPDATE GEBRUIKER SET Naam = @Naam, Adres = @Adres, Geboortedatum = @Geboortedatum WHERE GebruikerID = (SELECT GebruikerID FROM Login WHERE Username = @Gebruikernaam)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Naam", naam);
            cmd.Parameters.AddWithValue("@Adres", adres);
            cmd.Parameters.AddWithValue("@Geboortedatum", geboortedatum);
            cmd.Parameters.AddWithValue("@Gebruikernaam", gebruikernaam);
            cmd.ExecuteNonQuery();
        }
    }
}