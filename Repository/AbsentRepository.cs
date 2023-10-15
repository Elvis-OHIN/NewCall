using System.Data.SQLite;
using Config;
using Newtonsoft.Json;
using Spectre.Console;
using Models;


namespace Repository
{
    public class AbsentRepository {
        public static void GetAbsentByID(int student_id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                    
                using (var cmd = new SQLiteCommand("SELECT * FROM Student WHERE student_id = @student_id)", connection))
                {
                    cmd.Parameters.AddWithValue("@identifiant", student_id);
                    cmd.ExecuteNonQuery();

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["identifiant"]}, Name: {reader["password"]}");
                        }
                    }
                }
            }
        }
        public static SQLiteDataReader GetAbsentListByDate(DateTime date)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                    
                using (var cmd = new SQLiteCommand($"SELECT * FROM Absent WHERE date = '{date.Date}'", connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        return reader;
                    }
                }
            }
        }


        public static void FetchAbsentsByDate(DateTime date)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                    
                using (var cmd = new SQLiteCommand($"SELECT * FROM Absent WHERE date = '{date.Date}'", connection))
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
                                    Student student = StudentRepository.GetStudentByID(id);
                                    string lastname = student.Firstname;
                                    string firstname = student.Lastname;
                                    string statut  = student.Statut;
                                    table.AddRow(lastname,firstname,statut);
                                }
                            }
                        }
                        AnsiConsole.Write(table);
                    }
                }
            }
        }
        public static void addAbsent(DateTime date , object studentData)
        {
            string jsonData = JsonConvert.SerializeObject(studentData);

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                    
                using (var cmd = new SQLiteCommand($"INSERT INTO Absent (date,list) VALUES ('{date.Date}','{jsonData}')", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateAbsent(DateTime date , object studentData)
        {
            string jsonData = JsonConvert.SerializeObject(studentData);

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                    
                using (var cmd = new SQLiteCommand($"UPDATE Absent set list = '{jsonData}' WHERE date = '{date.Date}'", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int GetAbsentTotal(int user_id)
        {
            int total = 0;
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                    
                using (var cmd = new SQLiteCommand($"SELECT * FROM Absent", connection))
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
            }
            return total;
        }
    }
}
