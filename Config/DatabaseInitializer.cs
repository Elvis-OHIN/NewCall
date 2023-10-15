using System.Data.SQLite;

namespace Config
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            if (!TableExists("Students"))
            {
                
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand(@"
                        CREATE TABLE Students(
                                [user_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [firstname] NVARCHAR(50) NOT NULL,
                                [lastname] NVARCHAR(50) NOT NULL,
                                [statut] NVARCHAR(50) NULL
                        )", connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    
                    using (var cmd = new SQLiteCommand(@"INSERT INTO Students (firstname, lastname , statut) VALUES (@Firstname, @Lastname , @Statut)", connection))
                    {
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
            }

            if (!TableExists("Absent"))
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    using (var cmd = new SQLiteCommand(@"
                        CREATE TABLE Absent(
                            [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [date] DATE NOT NULL,
                            [list] DataJSON NULL
                        )", connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }


            if (!TableExists("Admin"))
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    using (var cmd = new SQLiteCommand(@"
                        CREATE TABLE Admin(
                            [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [identifiant] NVARCHAR(50) NOT NULL,
                            [password] NVARCHAR(50) NOT NULL
                        )", connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    using (var cmd = new SQLiteCommand(@"INSERT INTO Admin (identifiant,password) VALUES (@Identifiant , @Password)", connection))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Identifiant", "admin");
                        cmd.Parameters.AddWithValue("@Password", "password");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            if (!TableExists("Absences"))
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();

                    using (var cmd = new SQLiteCommand(@"
                        CREATE TABLE Absences (
                            [id] INTEGER PRIMARY KEY AUTOINCREMENT,   
                            [student_id] INTEGER,                         
                            [start_date] DATE NOT NULL,                   
                            [end_date] DATE,                           
                            [reason] TEXT,                                   
                            [comments] TEXT,                               
                            FOREIGN KEY (student_id) REFERENCES Students(user_id) 
                        )", connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public static bool TableExists(string tableName)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
    }

}
