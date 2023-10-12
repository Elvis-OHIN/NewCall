using System;
using Spectre.Console;

namespace Absent.Controller
{
    public class AbsentController
    {
        public static void DisplayAbsentList()
        {
            DateTime date = Calendars.Controller.CalendarController.ChooseDay();
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[yellow]Liste des absents[/]");
            Model.AbsentModel.FetchAbsentsByDate(date);
            AnsiConsole.MarkupLine("\r\n[green]Appuyez sur Entrée pour retourner au menu principal[/]");
            Console.ReadLine();
        }

        public static void DisplayAbsentListStats()
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[yellow]Statistiques des absences[/]");

                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("Nom");
                table.AddColumn("Prénom");
                table.AddColumn("Total des absences");

                System.Data.SQLite.SQLiteDataReader students = Student.Model.StudentModel.GetAllStudent();
                while (students.Read())
                {
                    int totalAbsences = Model.AbsentModel.GetAbsentTotal(Convert.ToInt32(students["user_id"]));
                    table.AddRow((string)students["firstname"], (string)students["lastname"], totalAbsences.ToString());
                }

                AnsiConsole.Write(table);
                AnsiConsole.MarkupLine("\r\n[green]Appuyez sur Entrée pour retourner au menu principal[/]");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Une erreur est survenue: {ex.Message}[/]");
            }
        }
    }
}
