using System.Data;
using System.Data.SqlClient;

namespace KillerApp_SE.DAL
{
    public static class Database
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-JKI4SS5;Initial Catalog=FUN2;Integrated Security=True";
        public static SqlConnection conn = new SqlConnection(connectionString);
        public static string exceptionMessage;

        public static void CheckConn()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }
    }
}