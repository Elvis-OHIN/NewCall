using System.Data.SQLite;
using Config;

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
    }
}
