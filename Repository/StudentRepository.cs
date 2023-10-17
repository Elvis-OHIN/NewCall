using System.Data.SQLite;
using Config;
using Models;
namespace Repository
{
    public class StudentRepository {

        public static Student GetStudentByID(int student_id)
        {
            Student student = new Student(0,"","","");
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                    
                using (var cmd = new SQLiteCommand("SELECT * FROM Students WHERE user_id = @student_id", connection))
                {
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            student = new Student 
                            (   
                                Convert.ToInt32(reader["user_id"]),
                                (string)reader["firstname"],
                                (string)reader["lastname"],
                                (string)reader["statut"]
                                
                            );
                            return student;
                        }
                    }
                }
            }
            return student;
        }

        public static List<Student> GetAllStudent()
        {
            var students = new List<Student>();

            using (var connection = Database.GetConnection())
            {
                connection.Open();

                using (var cmd = new SQLiteCommand("SELECT * FROM Students", connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student 
                        (   
                            Convert.ToInt32(reader["user_id"]),
                            (string)reader["firstname"],
                            (string)reader["lastname"],
                            (string)reader["statut"]
                            
                        );
                        students.Add(student);
                    }
                }
            }
            return students;
        }
    }
}
