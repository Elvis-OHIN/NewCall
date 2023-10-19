using System.Data.SQLite;
using System.Globalization;
using Config;
using Newtonsoft.Json;

namespace Repository
{
    public class AbsencesRepository {

        public static int GetTotalAbsencesCount()
        {
            string selectQuery = "SELECT COUNT(*) FROM Absences";
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(selectQuery, connection))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static double GetAverageAbsencesPerPerson()
        {
            string selectQuery = @"
                SELECT AVG(AbsenceCount) FROM (
                    SELECT student_id, COUNT(*) as AbsenceCount FROM Absences GROUP BY student_id
                )";
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(selectQuery, connection))
                {
                    return Convert.ToDouble(cmd.ExecuteScalar());
                }
            }
        }

        public static double GetMedianAbsences()
        {
            string selectQuery = @"
                SELECT AbsenceCount
                FROM (
                    SELECT student_id, COUNT(*) as AbsenceCount FROM Absences GROUP BY student_id ORDER BY AbsenceCount
                )
                LIMIT 1
                OFFSET (SELECT COUNT(*)/2 FROM Absences)";
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(selectQuery, connection))
                {
                    return Convert.ToDouble(cmd.ExecuteScalar());
                }
            }
        }

        public static int GetModeAbsences()
        {
            string selectQuery = @"
                SELECT AbsenceCount
                FROM (
                    SELECT COUNT(*) as AbsenceCount, COUNT(student_id) as CountFreq
                    FROM Absences
                    GROUP BY student_id
                )
                ORDER BY CountFreq DESC
                LIMIT 1";
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(selectQuery, connection))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static SQLiteDataReader GetAbsencesByWeekDay()
        {
            string selectQuery = @"
                SELECT strftime('%w', start_date) as WeekDay, COUNT(*) as AbsenceCount
                FROM Absences
                GROUP BY WeekDay
                ORDER BY WeekDay";
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(selectQuery, connection))
                {
                    return cmd.ExecuteReader();
                }
            }
        }

        public static bool AddAbsence(int personID, DateTime startDate, DateTime? endDate, string reason, string comments)
        {
            string insertQuery = @"
                INSERT INTO Absences (student_id, start_date, end_date, reason, comments)
                VALUES (@personID, @startDate, @endDate, @reason, @comments)";

            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@personID", personID);
                    cmd.Parameters.AddWithValue("@startDate", startDate.Date);
                    cmd.Parameters.AddWithValue("@endDate", endDate?.Date);
                    cmd.Parameters.AddWithValue("@reason", reason);
                    cmd.Parameters.AddWithValue("@comments", comments);
                    int result = cmd.ExecuteNonQuery();
                    return result == 1;
                }
            }
        }
        public static bool DeleteAbsence(int personID, DateTime startDate, DateTime? endDate)
        {
            string insertQuery = @"
                DELETE from Absences where student_id = @personID and start_date = @startDate and end_date = @endDate";
            DateTime parsedDate = DateTime.ParseExact(startDate.Date.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string formattedDate = parsedDate.ToString("yyyy-MM-dd HH:mm:ss");
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@personID", personID);
                    cmd.Parameters.AddWithValue("@startDate", formattedDate);
                    cmd.Parameters.AddWithValue("@endDate", formattedDate);
                    int result = cmd.ExecuteNonQuery();
                    return result == 1;
                }
            }
        }
        public static string[] DeleteAbsenceByDay(DateTime date){
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                DateTime parsedDate = DateTime.ParseExact(date.Date.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string formattedDate = parsedDate.ToString("yyyy-MM-dd");
                string formattedDate2 = parsedDate.ToString("yyyy-MM-dd HH:mm:ss");

                using (var cmd = new SQLiteCommand($"SELECT * FROM AbsenceList WHERE date = '{formattedDate}'", connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        string[] ids = {};
                        while (reader.Read())
                        {
                            // Supprimez les crochets et séparez les identifiants
                            var list = "[]";
                            list = (string)reader["list"];
                            string listWithoutBrackets = list.ToString().Trim('[', ']');
                            ids = listWithoutBrackets.Split(',');
                        }
                        return ids;
                    }
                }

            }


        }
        public static void AddAbsenceByList(DateTime date , object studentData)
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

    }
}
