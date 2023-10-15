using System;
using Spectre.Console;
using Repository;
using Models;

namespace Controller
{
    public class AbsentController
    {
        public static void DisplayAbsentList()
        {
            DateTime date = CalendarController.ChooseDay();
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[yellow]Liste des absents[/]");
            AbsentRepository.FetchAbsentsByDate(date);
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

                List<Student> students = StudentRepository.GetAllStudent();
                foreach (var student in students)
                {
                    int totalAbsences = AbsentRepository.GetAbsentTotal(student.Id);
                    table.AddRow(student.Firstname, student.Lastname, totalAbsences.ToString());
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
