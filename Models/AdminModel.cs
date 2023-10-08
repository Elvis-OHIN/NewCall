using System.Data.SQLite;
using Database.Config;

namespace Admin.Model
{
    public class AdminModel {
        private static readonly string connectionString = Database.Config.Database.connectionString;
        private static SQLiteConnection connection;

        static AdminModel()
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => CloseConnection();
        }

        public static void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
        public static bool GetAdmin(string identifiant, string password)
        {
            string selectQuery = "SELECT * FROM Admin WHERE identifiant = @identifiant and password = @password";

            using (var cmd = new SQLiteCommand(selectQuery, connection))
            {
                cmd.Parameters.AddWithValue("@identifiant", identifiant);
                cmd.Parameters.AddWithValue("@password", password);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
