using System.Data.SQLite;

namespace Database.Config
{
    public class Database
    {
        public static readonly string connectionString = "Data Source=NewCall.db;Version=3;";
        public static SQLiteConnection connection;
        public static SQLiteConnection Connection => connection;

        static Database()
        {
            connection = new SQLiteConnection(connectionString);
            OpenConnection();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => CloseConnection();
        }

        public static void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }


        public static void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public static bool TableExists(string tableName)
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";
                using (var reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        public static void EnsureTableExists()
        {
            if (!TableExists("Students"))
            {
                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        CREATE TABLE Students(
                            [user_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [firstname] NVARCHAR(50) NOT NULL,
                            [lastname] NVARCHAR(50) NOT NULL,
                            [statut] NVARCHAR(50) NULL
                        );
                    ";
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Students (firstname, lastname , statut) VALUES (@Firstname, @Lastname , @Statut)";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Firstname", "Maxime");
                    cmd.Parameters.AddWithValue("@Lastname", "CHABEAUDIE");
                    cmd.Parameters.AddWithValue("@Statut", "FA");
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Firstname", "Léo");
                    cmd.Parameters.AddWithValue("@Lastname", "FETTER");
                    cmd.Parameters.AddWithValue("@Statut", "FE");
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Firstname", "Florian");
                    cmd.Parameters.AddWithValue("@Lastname", "MONTASTIER");
                    cmd.Parameters.AddWithValue("@Statut", "FA");
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Firstname", "Jérémy");
                    cmd.Parameters.AddWithValue("@Lastname", "MARTIN");
                    cmd.Parameters.AddWithValue("@Statut", "FE");
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Firstname", "ELvis");
                    cmd.Parameters.AddWithValue("@Lastname", "OHIN");
                    cmd.Parameters.AddWithValue("@Statut", "FA");
                    cmd.ExecuteNonQuery();
                }
            }

            if (!TableExists("Absent"))
            {
                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        CREATE TABLE Absent(
                            [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [date] DATE NOT NULL,
                            [list] DataJSON NULL
                        );
                    ";
                    cmd.ExecuteNonQuery();
                }
            }


            if (!TableExists("Admin"))
            {
                using (var cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        CREATE TABLE Admin(
                            [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [identifiant] NVARCHAR(50) NOT NULL,
                            [password] NVARCHAR(50) NOT NULL
                        );
                    ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Admin (identifiant,password) VALUES (@Identifiant , @Password)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Identifiant", "admin");
                    cmd.Parameters.AddWithValue("@Password", "password");
                    cmd.ExecuteNonQuery();

                }

            }
        }
    }
}
