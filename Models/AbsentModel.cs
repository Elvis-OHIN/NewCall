using System.Data.SQLite;
using Database.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spectre.Console;


namespace Absent.Model
{
    public class AbsentModel {

        private static readonly string connectionString = Database.Config.Database.connectionString;

        public static SQLiteConnection CreateConnection()
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
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
        public static SQLiteDataReader GetAbsentListByDate(DateTime date)
        {
            string selectQuery = $"SELECT * FROM Absent WHERE date = '{date.Date}'";
            using var connection = CreateConnection();
            using var cmd = new SQLiteCommand(selectQuery, connection);
            using SQLiteDataReader reader = cmd.ExecuteReader();
            return reader;
        }


        public static void FetchAbsentsByDate(DateTime date)
        {
            using var connection = CreateConnection();
            string selectQuery = $"SELECT * FROM Absent WHERE date = '{date.Date}'";

            using (var cmd = new SQLiteCommand(selectQuery, connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    var table = new Table();
                    table.AddColumn(new TableColumn("Nom").Centered());
                    table.AddColumn(new TableColumn("Prénom").Centered());
                    table.AddColumn(new TableColumn("Statut").Centered());
                    table.Border(TableBorder.Rounded);

                    while (reader.Read())
                    {
                        // Supprimez les crochets et séparez les identifiants
                        var list = "[]";
                        list = (string)reader["list"];
                        string listWithoutBrackets = list.ToString().Trim('[', ']');
                        string[] ids = listWithoutBrackets.Split(',');

                        foreach (string idString in ids)
                        {
                            if (int.TryParse(idString.Trim(), out int id))
                            {
                                SQLiteDataReader student = Student.Model.StudentModel.GetStudentByID(id);
                                if (student.HasRows){
                                    while (student.Read()) {
                                        string lastname = (string)student["lastname"];
                                        string firstname = (string)student["firstname"];
                                        string statut  = (string)student["statut"];
                                        table.AddRow(lastname,firstname,statut);
                                    }
                                }
                            }
                        }
                    }
                    AnsiConsole.Write(table);
                }
            }
        }
        public static void addAbsent(DateTime date , object studentData)
        {
            string jsonData = JsonConvert.SerializeObject(studentData);
            using var connection = CreateConnection();
            string insertQuery = $"INSERT INTO Absent (date,list) VALUES ('{date.Date}','{jsonData}')";
            using var cmd = new SQLiteCommand(insertQuery, connection);
            cmd.ExecuteNonQuery();
        }
        public static void UpdateAbsent(DateTime date , object studentData)
        {
            using var connection = CreateConnection();
            string jsonData = JsonConvert.SerializeObject(studentData);
            string insertQuery = $"UPDATE Absent set list = '{jsonData}' WHERE date = '{date.Date}'";
            using var cmd = new SQLiteCommand(insertQuery, connection);
            cmd.ExecuteNonQuery();
        }
        public static string GetAbsentList()
        {
            using var connection = CreateConnection();
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

        public static int GetAbsentTotal(int user_id)
        {
            int total = 0;
            using var connection = CreateConnection();

            string selectQuery = "SELECT * FROM Absent";
            using (var cmd = new SQLiteCommand(selectQuery, connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var list = "[]";
                        list = (string)reader["list"];
                        string listWithoutBrackets = list.ToString().Trim('[', ']');
                        string[] ids = listWithoutBrackets.Split(',');
                        foreach (string idString in ids)
                        {
                            if (int.TryParse(idString.Trim(), out int id))
                            {
                                if(id == user_id)
                                {
                                    total++;
                                }
                            }
                        }
                    }
                }
            }
            return total;
        }
    }
}
