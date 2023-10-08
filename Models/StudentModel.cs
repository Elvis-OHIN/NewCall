using System.Data.SQLite;

namespace Student.Model
{
    public class StudentModel {

        private static readonly string connectionString = Database.Config.Database.connectionString;
        private static SQLiteConnection connection;

        static StudentModel()
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

        public static void GetStudentByID(int student_id)
        {
            string selectQuery = "SELECT * FROM Students WHERE user_id = @student_id";

            using (var cmd = new SQLiteCommand(selectQuery, connection))
            {
                cmd.Parameters.AddWithValue("@student_id", student_id);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"Prénom: {reader["firstname"]}, Nom de famille: {reader["lastname"]}");
                    }
                }
            }
        }
        public static SQLiteDataReader GetAllStudent()
        {
            string selectQuery = "SELECT * FROM Students";
            var cmd = new SQLiteCommand(selectQuery, connection);
            return cmd.ExecuteReader();
        }
    }
}
