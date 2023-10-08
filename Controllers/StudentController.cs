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

        public static void Call()
        {
            Console.Clear();
            String? s;
            List<int> array = new List<int>();
            DateTime date = ChoiceDay();

            using (SQLiteDataReader students = Student.Model.StudentModel.GetAllStudent())
            {
                while (students.Read())
                {
                    do
                    {
                        Console.WriteLine($"L'étudiant {students["firstname"]} {students["lastname"]} est-il absent ou présent ? Tapez 'a' pour absent ou 'p' pour présent");
                        s = Console.ReadLine();
                        switch (s)
                        {
                            case "a":
                            case "A":
                                s = "ok";
                                array.Add(Convert.ToInt32(students["user_id"]));
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
                var studentInfo = new
                {
                    array
                };
                Absent.Model.AbsentModel.addAbsent(date, studentInfo);
            }
        }

         public static DateTime ChoiceDay()
        {
            Console.Clear();
            string s;
            bool validDate;
            do
            {
                Console.WriteLine($"Veuillez entrer une date pour l'appel (format JJ/MM/AAAA) :");
                s = Console.ReadLine();
                validDate = IsValidDate(s, out DateTime parsedDate);
                if (validDate)
                {
                    return parsedDate;
                }
            } while (!validDate);

            // Ceci ne sera jamais atteint, mais c'est nécessaire pour satisfaire le compilateur.
            return DateTime.MinValue;
        }

        public static bool IsValidDate(string date, out DateTime parsedDate)
        {
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Date non valide. Assurez-vous d'utiliser le format JJ/MM/AAAA");
                return false;
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
