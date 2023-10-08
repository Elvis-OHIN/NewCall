using System.Data.SQLite;
using Database.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Absent.Model
{
    public class AbsentModel {

        private static readonly string connectionString = Database.Config.Database.connectionString;
        private static SQLiteConnection connection;

        static AbsentModel()
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
        public static void GetAbsentByID(int student_id)
        {
            string selectQuery = "SELECT * FROM Student WHERE student_id = @student_id";
            using var connection = Database.Config.Database.Connection;
            using (var cmd = new SQLiteCommand(selectQuery, connection))
            {
                cmd.Parameters.AddWithValue("@identifiant", student_id);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["identifiant"]}, Name: {reader["password"]}");
                    }
                }
            }
        }
        public static string GetAbsentListByDate(DateTime date)
        {
            List<int> ids = new List<int>();
            string selectQuery = "SELECT * FROM Absent WHERE date = @date";
            using var connection = Database.Config.Database.Connection;
            using var cmd = new SQLiteCommand(selectQuery, connection);
            using SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ids.Add(Convert.ToInt32(reader["id"]));
            }
            return string.Join(",", ids);
        }

        public static void InsertA(string firstname,string lastname,string statut)
        {
            string insertQuery = $"INSERT INTO Absent (firstname,lastname,statut) VALUES ('{firstname}','{lastname}','{statut}')";
            using var cmd = new SQLiteCommand(insertQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public static void FetchAbsentsByDate(DateTime date)
        {
            // Assuming the connection is static within the Database class
            SQLiteConnection connection = Database.Config.Database.Connection;

            string selectQuery = "SELECT * FROM Absent";
            using (var cmd = new SQLiteCommand(selectQuery, connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string jsonList = (string)reader["list"];
                        JObject jsonObject = JObject.Parse(jsonList);
                        JArray jsonArray = (JArray)jsonObject["array"];
                        foreach (int id in jsonArray)
                        {
                            Student.Model.StudentModel.GetStudentByID(id);
                        }
                    }
                }
            }
        }

        public static void addAbsent(DateTime date , object studentData)
        {
            string jsonData = JsonConvert.SerializeObject(studentData);
            string insertQuery = $"INSERT INTO Absent (date,list) VALUES ('{date}','{jsonData}')";
            using var cmd = new SQLiteCommand(insertQuery, connection);
            cmd.ExecuteNonQuery();
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
    }
}
