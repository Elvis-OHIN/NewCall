using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Students.Controller
{
    public class Code
    {
        private static readonly string connectionString = "Data Source=NewCall.db;Version=3;";
        private static readonly SQLiteConnection connection;

        static Code()
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => CloseConnection();
        }

        public static void Call()
        {
            Console.Clear();
            List<int> absentStudents = new List<int>();
            DateOnly date = ChooseDay();

            using (SQLiteDataReader students = Student.Model.StudentModel.GetAllStudent())
            {
                while (students.Read())
                {
                    char response;
                    do
                    {
                        Console.WriteLine($"L'étudiant {students["firstname"]} {students["lastname"]} est-il absent ou présent ? (a pour absent, p pour présent)");
                        response = Char.ToLower(Console.ReadKey().KeyChar);
                        Console.WriteLine();  // New line for better user experience.

                        if (response == 'a')
                        {
                            absentStudents.Add(Convert.ToInt32(students["user_id"]));
                            Console.WriteLine($"Absent");
                        }
                        else if (response == 'p')
                        {
                            Console.WriteLine($"Présent");
                        }
                        else
                        {
                            Console.WriteLine($"Erreur. Taper 'a' ou 'p'");
                        }
                    } while (response != 'a' && response != 'p');
                }
                Absent.Model.AbsentModel.addAbsent(date, absentStudents);
            }
        }

        public static DateOnly ChooseDay()
        {
            Console.Clear();
            DateOnly parsedDate;
            string? inputDate;

            do
            {
                Console.WriteLine("Veuillez entrer une date pour l'appel (format JJ/MM/AAAA) :");
                inputDate = Console.ReadLine();

            } while (!IsValidDate(inputDate, out parsedDate));

            return parsedDate;
        }

        public static bool IsValidDate(string date, out DateOnly parsedDate)
        {
            if (DateOnly.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Date non valide. Assurez-vous d'utiliser le format JJ/MM/AAAA");
                return false;
            }
        }

        public static void InsertAbsentee(string firstname, string lastname, string status)
        {
            string insertQuery = "INSERT INTO Absent (firstname, lastname, statut) VALUES (@firstname, @lastname, @statut)";
            using var cmd = new SQLiteCommand(insertQuery, connection);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@statut", status);
            cmd.ExecuteNonQuery();
        }

        public static void GetUsers()
        {
            const string selectQuery = "SELECT * FROM User";
            using var cmd = new SQLiteCommand(selectQuery, connection);
            using SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["identifiant"]}, Password: {reader["password"]}");
            }
        }

        public static void GetAbsentees()
        {
            const string selectQuery = "SELECT * FROM Absent";
            using var cmd = new SQLiteCommand(selectQuery, connection);
            using SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Firstname: {reader["firstname"]}, Lastname: {reader["lastname"]}, Status: {reader["statut"]}");
            }
        }

        public static bool AuthenticateUser(string username, string password)
        {
            const string selectQuery = "SELECT * FROM User WHERE identifiant = @identifiant AND password = @password";

            using var cmd = new SQLiteCommand(selectQuery, connection);
            cmd.Parameters.AddWithValue("@identifiant", username);
            cmd.Parameters.AddWithValue("@password", password);

            using SQLiteDataReader reader = cmd.ExecuteReader();
            return reader.Read();
        }

        public static void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public static void ClearAbsentees()
        {
            const string deleteQuery = "DELETE FROM Absent";
            using var cmd = new SQLiteCommand(deleteQuery, connection);
            cmd.ExecuteNonQuery();
        }
    }
}
