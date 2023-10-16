using System;
using Spectre.Console;
using Repository;
using Models;

namespace Controller
{
    public class AbsenceController
    {
        public static void DisplayAbsenceListList()
        {
            DateTime date = CalendarController.ChooseDay();
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[yellow]Liste des Absents[/]");
            AbsenceListRepository.FetchAbsenceListsByDate(date);
            AnsiConsole.MarkupLine("\r\n[green]Appuyez sur Entrée pour retourner au menu principal[/]");
            Console.ReadLine();
        }

        public static void DisplayAbsenceListListStats()
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
                    int totalAbsences = AbsenceListRepository.GetAbsenceListTotal(student.Id);
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

        public static void DisplayAbsencesGlobalStats()
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[yellow]Statistiques des absences[/]");

                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("Nombre total d'absences");
                table.AddColumn("Moyenne d'absences par individu");
                table.AddColumn("Médiane d'absences");
                table.AddColumn("Mode d'absences");

                int TotalAbsencesCount = AbsencesRepository.GetTotalAbsencesCount();
                double AverageAbsencesPerPerson =  AbsencesRepository.GetAverageAbsencesPerPerson();
                double MedianAbsences =  AbsencesRepository.GetMedianAbsences();
                int ModeAbsences =   AbsencesRepository.GetModeAbsences();
                table.AddRow(
                    TotalAbsencesCount.ToString(),
                    AverageAbsencesPerPerson.ToString(),
                    MedianAbsences.ToString(),
                    ModeAbsences.ToString()
                );
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
