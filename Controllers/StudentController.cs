using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Students.Controller
{
    public class Code
    {
        private static readonly string connectionString = "Data Source=NewCall.db;Version=3;";
        private static SQLiteConnection connection;

        static Code()
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => CloseConnection();
        }

        public static void ListAbsents()
        {
            Console.Clear();
            List<Student> absents = new List<Student>();
            Console.WriteLine($"Liste des absents");
            GetA();
            Console.Write("\r\nAppuyez sur Entrée pour retourner au menu principal");
            Console.ReadLine();
        }

        public static void Call()
        {
            Console.Clear();
            String? s;
            List<Student> students = new List<Student>();
            students.Add(new Student("CHABEAUDIE" , "Maxime"));
            students.Add(new Student("FETTER" , "Léo"));
            students.Add(new Student("MONTASTIER" , "Florian"));
            students.Add(new Student("MARTIN" , "Jérémy"));
            students.Add(new Student("OHIN" , "ELvis"));

            foreach (Student studentCurrent in students)
            {
                do
                {
                    Console.WriteLine($"L'étudiant {studentCurrent.LastName} {studentCurrent.Firstname} est-il absent ou présent ? Tapez 'a' pour absent ou 'p' pour présent");
                    s = Console.ReadLine();
                    switch (s)
                    {
                        case "a":
                        case "A":
                            InsertA(studentCurrent.Firstname,studentCurrent.LastName);
                            s = "ok";
                            Console.WriteLine($"Absent");
                            break;
                        case "p":
                        case "P":
                            s = "ok";
                            Console.WriteLine($"Présent");
                            break;
                        default:
                            Console.WriteLine($"Erreur. Taper 'a' ou 'p'");
                            break;
                    }
                } while (s != "ok");
            }
            ListAbsents();
        }

        public static void Data()
        {
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [User] (
                          [user_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [firstname] NVARCHAR(50) NOT NULL,
                          [lastname] NVARCHAR(50) NOT NULL,
                          [identifiant] NVARCHAR(255) NULL,
                          [password] NVARCHAR(255) NULL,
                          [role] NVARCHAR(50) NULL
                          )";
            using var cmd = new SQLiteCommand(createTableQuery, connection);
            cmd.ExecuteNonQuery();
        }
        public static void DataA()
        {
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [Absent] (
                          [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [firstname] NVARCHAR(50) NOT NULL,
                          [lastname] NVARCHAR(50) NOT NULL
                          )";
            using var cmd = new SQLiteCommand(createTableQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public static void Insert()
        {
            string insertQuery = "INSERT INTO User (firstname,lastname,identifiant,password,role) VALUES ('admin','admin','admin','password','admin')";
            using var cmd = new SQLiteCommand(insertQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public static void InsertA(string firstname,string lastname)
        {
            string insertQuery = $"INSERT INTO Absent (firstname,lastname) VALUES ('{firstname}','{lastname}')";
            using var cmd = new SQLiteCommand(insertQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public static void Get()
        {
            string selectQuery = "SELECT * FROM User";
            using var cmd = new SQLiteCommand(selectQuery, connection);
            using SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["identifiant"]}, Name: {reader["password"]}");
            }
        }
        public static void GetA()
        {
            string selectQuery = "SELECT * FROM Absent";
            using var cmd = new SQLiteCommand(selectQuery, connection);
            using SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Firstname: {reader["firstname"]}, Lastname: {reader["lastname"]}");
            }
        }

        public static bool GetUserByName(string identifiant,string password)
        {

        string selectQuery = "SELECT * FROM User WHERE identifiant = @identifiant and password = @password";

        using var cmd = new SQLiteCommand(selectQuery, connection);
                cmd.Parameters.AddWithValue("@identifiant", identifiant);
                cmd.Parameters.AddWithValue("@password", password);

        using SQLiteDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return true;
        }

        return false;
        }

        public static void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public static void Clear(){
            string deleteQuery = "DELETE FROM Absent";
            using var cmd = new SQLiteCommand(deleteQuery, connection);
            cmd.ExecuteNonQuery();
        }
    }


}
