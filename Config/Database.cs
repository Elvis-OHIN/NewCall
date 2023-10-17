using System.Data.SQLite;

namespace Config
{
    public class Database
    {
        private const string ConnectionString = "Data Source=NewCall.db;Version=3;DateTimeFormat=ISO8601;";
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }
    }
}
