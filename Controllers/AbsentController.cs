using System;

namespace Absent.Controller
{
    public class AbsentController
    {
        // Display the list of absentees on the screen.
        public static void DisplayAbsentList()
        {


                DateOnly date = ChooseDay();
                Console.Clear();
                Console.WriteLine("Liste des absents");

                // Retrieve and display the list of absentees from the model.
                Model.AbsentModel.FetchAbsentsByDate(date);

                Console.Write("\r\nPress Enter to return to the main menu");
                Console.ReadLine();

        }
        public static DateOnly ChooseDay()
        {
            Console.Clear();
            string inputDate;
            bool isValidDate;
            DateOnly parsedDate;

            do
            {
                Console.WriteLine("Please enter a date for the roll call (format DD/MM/YYYY):");
                inputDate = Console.ReadLine();
                isValidDate = TryParseDate(inputDate, out parsedDate);

                if (isValidDate)
                {
                    return parsedDate;
                }
            } while (!isValidDate);

            return DateOnly.MinValue;
        }


        public static bool TryParseDate(string date, out DateOnly parsedDate)
        {
            if (DateOnly.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Invalid date. Please ensure you use the format DD/MM/YYYY");
                return false;
            }
        }

        public static void DisplayAbsentListStats()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Absentee statistics");

                System.Data.SQLite.SQLiteDataReader students = Student.Model.StudentModel.GetAllStudent();
                while (students.Read())
                {
                    int totalAbsences = Model.AbsentModel.GetAbsentTotal((int)Convert.ToInt64(students["user_id"]));
                    Console.WriteLine($"The student {students["firstname"]} {students["lastname"]} has been absent {totalAbsences} times");
                }

                Console.Write("\r\nPress Enter to return to the main menu");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
