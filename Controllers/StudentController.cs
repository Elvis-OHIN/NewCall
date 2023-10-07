using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Security;
using Calendar.Controller;

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
            String date;
            List<Student> students = new List<Student>();
            students.Add(new Student("CHABEAUDIE" , "Maxime"));
            students.Add(new Student("FETTER" , "Léo"));
            students.Add(new Student("MONTASTIER" , "Florian"));
            students.Add(new Student("MARTIN" , "Jérémy"));
            students.Add(new Student("OHIN" , "ELvis"));
            date = ChoiceDay();

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
                            InsertA(studentCurrent.Firstname,studentCurrent.LastName,"FA");
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
            Calendar.Controller.Code.Insert(date,GetAbsentList());
            ListAbsents();
            Clear();
        }
        public static string ChoiceDay()
        {
            Console.Clear();
            String? s;
            bool verif;
            do
                {
                    Console.WriteLine($"Veuillez entrer une date pour l'appel (format JJ/MM/AAAA) :");
                    s = Console.ReadLine();
                    verif = VerifSaisie(s);
                } while (verif != true);

            return s;
        }

        public static bool VerifSaisie(string date){
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Date non valide. Assurez-vous d'utiliser le format JJ/MM/AAAA");
                return false;
            }
        }

        public static void Data()
        {
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [User] (
                          [user_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [firstname] NVARCHAR(50) NOT NULL,
                          [lastname] NVARCHAR(50) NOT NULL,
                          [identifiant] NVARCHAR(255) NULL,
                          [password] NVARCHAR(255) NULL,
                          [role] NVARCHAR(50) NULL,
                          [statut] NVARCHAR(50) NULL
                          )";
            using var cmd = new SQLiteCommand(createTableQuery, connection);
            cmd.ExecuteNonQuery();
        }
        public static void DataA()
        {
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [Absent] (
                          [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [firstname] NVARCHAR(50) NOT NULL,
                          [lastname] NVARCHAR(50) NOT NULL,
                          [statut] NVARCHAR(50) NULL
                          )";
            using var cmd = new SQLiteCommand(createTableQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public static void Insert()
        {
           // string insertQuery = "INSERT INTO User (firstname,lastname,identifiant,password,role) VALUES ('admin','admin','admin','password','admin')";
            //using var cmd = new SQLiteCommand(insertQuery, connection);
            //cmd.ExecuteNonQuery();
            List<Student> students = new List<Student>();
            students.Add(new Student("CHABEAUDIE" , "Maxime"));
            students.Add(new Student("FETTER" , "Léo"));
            students.Add(new Student("MONTASTIER" , "Florian"));
            students.Add(new Student("MARTIN" , "Jérémy"));
            students.Add(new Student("OHIN" , "ELvis"));

            foreach (Student studentCurrent in students)  {
                string insertQuery = $"INSERT INTO User (firstname,lastname,role) VALUES ('{studentCurrent.LastName}','{studentCurrent.Firstname}','student')";
                using var cmd = new SQLiteCommand(insertQuery, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public static void InsertA(string firstname,string lastname,string statut)
        {
            string insertQuery = $"INSERT INTO Absent (firstname,lastname,statut) VALUES ('{firstname}','{lastname}','{statut}')";
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
                Console.WriteLine($"Firstname: {reader["firstname"]}, Lastname: {reader["lastname"]} , {reader["statut"]}");
            }
        }
        public static string GetAbsentList()
        {
            List<int> ids = new List<int>();
            string selectQuery = "SELECT * FROM Absent";
            using var cmd = new SQLiteCommand(selectQuery, connection);
            using SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ids.Add(Convert.ToInt32(reader["id"]));
            }
            return string.Join(",", ids);
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
