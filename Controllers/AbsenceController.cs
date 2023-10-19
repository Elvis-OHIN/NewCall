using System;
using Spectre.Console;
using Repository;
using Models;

namespace Controller
{
    public class AbsenceController
    {
        // Affiche la liste des absences pour une date choisie
        public static void DisplayAbsenceListList()
        {
            try
            {
                DateTime date = CalendarController.ChooseDay();
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[yellow]Liste des Absents[/]");
                AbsenceListRepository.FetchAbsenceListsByDate(date);
                AnsiConsole.MarkupLine("\r\n[green]Appuyez sur Entrée pour retourner au menu principal[/]");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Une erreur est survenue: {ex.Message}[/]");
            }
        }

        // Affiche les statistiques des absences par étudiant
        public static void DisplayAbsenceListListStats()
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[yellow]Statistiques des absences[/]");

                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("Nom de l'étudiant");
                table.AddColumn("Prénom de l'étudiant");
                table.AddColumn("Nombre d'absences");

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

        // Affiche les statistiques globales des absences
        public static void DisplayAbsencesGlobalStats()
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[yellow]Statistiques des absences[/]");

                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("Total des absences");
                table.AddColumn("Moyenne d'absences/étudiant");
                table.AddColumn("Médiane des absences");

                int TotalAbsencesCount = AbsencesRepository.GetTotalAbsencesCount();
                double AverageAbsencesPerPerson =  AbsencesRepository.GetAverageAbsencesPerPerson();
                double MedianAbsences =  AbsencesRepository.GetMedianAbsences();
                int ModeAbsences =   AbsencesRepository.GetModeAbsences();
                table.AddRow(
                    TotalAbsencesCount.ToString(),
                    AverageAbsencesPerPerson.ToString(),
                    MedianAbsences.ToString()
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

