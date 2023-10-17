using System.Data.SQLite;
using Config;

namespace Repository
{
    public class AdminRepository {
        

        public static bool GetAdmin(string identifiant, string password)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                    
                using (var cmd = new SQLiteCommand("SELECT * FROM Admin WHERE identifiant = @identifiant and password = @password", connection))
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
            }
            return false;
        }
    }
}
