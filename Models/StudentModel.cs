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

        public static SQLiteDataReader GetStudentByID(int student_id)
        {
            string selectQuery = "SELECT * FROM Students WHERE user_id = @student_id";
            var cmd = new SQLiteCommand(selectQuery, connection);
            cmd.Parameters.AddWithValue("@student_id", student_id);
            var reader = cmd.ExecuteReader();
            return reader;
        }


        public static SQLiteDataReader GetAllStudent()
        {
            string selectQuery = "SELECT * FROM Students";
            var cmd = new SQLiteCommand(selectQuery, connection);
            return cmd.ExecuteReader();
        }
    }
}
