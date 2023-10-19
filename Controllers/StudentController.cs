using System;
using System.Collections.Generic;
using Models;
using Spectre.Console;
using Repository;

namespace Controller
{
    public class StudentController
    {
        public static void Call()
        {
            AnsiConsole.Clear();
            List<int> AbsenceListStudents = new List<int>();
            DateTime date = CalendarController.ChooseDay();
            AnsiConsole.Clear();

            List<Student> students = StudentRepository.GetAllStudent();

            foreach (var student in students)
            {
                char response;
                do
                {
                    response = AnsiConsole.Ask<char>($"L'étudiant {student.Firstname} {student.Lastname} en {student.Statut} est-il [yellow]absent[/] (a) ou [yellow]présent[/] (p) ?");
                    response = char.ToLower(response);

                    switch (response)
                    {
                        case 'a':
                            AbsenceListStudents.Add(student.Id);
                            AbsencesRepository.AddAbsence(student.Id, date.Date, date.Date, "", "");
                            AnsiConsole.MarkupLine("[red]Absent[/]");
                            break;
                        case 'p':
                            AnsiConsole.MarkupLine("[green]Présent[/]");

                            string studentStatus = student.Statut;
                            if (studentStatus == "FA")
                            {
                                AnsiConsole.MarkupLine("[yellow]L'étudiant doit signer la feuille d'émargement.[/]");
                            }
                            break;
                        default:
                            AnsiConsole.MarkupLine("[yellow]Erreur. Veuillez taper 'a' pour absent ou 'p' pour présent.[/]");
                            break;
                    }
                } while (response != 'a' && response != 'p');
            }

            var AbsenceList = AbsenceListRepository.GetAbsenceListListByDate(date.Date);
            if (!AbsenceList.HasRows)
            {
                AbsenceListRepository.addAbsenceList(date, AbsenceListStudents);
            }
            else
            {

                foreach (string item in AbsencesRepository.DeleteAbsenceByDay(date))
                {
                    if (int.TryParse(item.Trim(), out int id))
                        {
                            AbsencesRepository.DeleteAbsence(id,date,date);
                        }
                }
                foreach (int id in AbsenceListStudents)
                {
                    AbsencesRepository.AddAbsence(id, date.Date, date.Date, "", "");
                }
                AbsenceListRepository.UpdateAbsenceList(date, AbsenceListStudents);


            }
            AnsiConsole.MarkupLine("\r\n[green]Appuyez sur Entrée pour retourner au menu principal[/]");
            Console.ReadLine();
            AnsiConsole.Clear();
        }
    }
}
