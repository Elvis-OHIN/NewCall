using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Calendar.Controller
{
    public class Code
    {
        private static readonly string connectionString = "Data Source=NewCall.db;Version=3;";
        private static SQLiteConnection connection;

        static Code()
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => CloseConnection();
        }

        public static void Data()
        {
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [Calendar] (
                          [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [date] NVARCHAR(50) NOT NULL,
                          [absent_list] DataJSON NULL
                          )";
            using var cmd = new SQLiteCommand(createTableQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public static void Insert(string date,string absentList)
        {
            string insertQuery = $"INSERT INTO Calendar(date,absent_list) VALUES ('{date}','{absentList}')";
            using var cmd = new SQLiteCommand(insertQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public static void GetA()
        {
            string selectQuery = "SELECT * FROM Absent";
            using var cmd = new SQLiteCommand(selectQuery, connection);
            using SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Firstname: {reader["firstname"]}, Lastname: {reader["lastname"]} , {reader["statut"]}");
            }
        }




        public static void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public static void Clear(){
            string deleteQuery = "DELETE FROM Absent";
            using var cmd = new SQLiteCommand(deleteQuery, connection);
            cmd.ExecuteNonQuery();
        }
    }


}
