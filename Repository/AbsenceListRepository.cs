using System.Data.SQLite;
using Config;
using Newtonsoft.Json;
using Spectre.Console;
using Models;
using System.Globalization;


namespace Repository
{
    public class AbsenceListRepository {
        public static void GetAbsenceListByID(int student_id)
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
        public static SQLiteDataReader GetAbsenceListListByDate(DateTime date)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand($"SELECT * FROM AbsenceList WHERE date = '{date.ToString("yyyy-MM-dd")}'", connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        return reader;
                    }
                }
            }
        }


        public static void FetchAbsenceListsByDate(DateTime date)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                DateTime parsedDate = DateTime.ParseExact(date.Date.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string formattedDate = parsedDate.ToString("yyyy-MM-dd");

                using (var cmd = new SQLiteCommand($"SELECT * FROM AbsenceList WHERE date = '{formattedDate}'", connection))
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

        public static List<DateTime> GetDateAbsenceList()
        {
            List<DateTime> dateList =  new List<DateTime>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand($"SELECT * FROM AbsenceList", connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dateList.Add((DateTime)reader["date"]);
                        }
                    }
                }
            }
            return dateList;
        }
        public static void addAbsenceList(DateTime date , object studentData)
        {
            string jsonData = JsonConvert.SerializeObject(studentData);

            using (var connection = Database.GetConnection())
            {
                connection.Open();

                DateTime parsedDate = DateTime.ParseExact(date.Date.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string formattedDate = parsedDate.ToString("yyyy-MM-dd");

                using (var cmd = new SQLiteCommand($"INSERT INTO AbsenceList (date,list) VALUES ('{formattedDate}','{jsonData}')", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateAbsenceList(DateTime date , object studentData)
        {
            string jsonData = JsonConvert.SerializeObject(studentData);

            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand($"UPDATE AbsenceList set list = '{jsonData}' WHERE date = '{date.ToString("yyyy-MM-dd")}'", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static int GetAbsenceListTotal(int user_id)
        {
            int total = 0;
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand($"SELECT * FROM AbsenceList", connection))
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
