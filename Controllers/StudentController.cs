using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Spectre.Console;
using Calendars;

namespace Students.Controller
{
    public class Code
    {
        public static void Call()
        {
            AnsiConsole.Clear();
            List<int> absentStudents = new List<int>();
            DateTime date = Calendars.Controller.CalendarController.ChooseDay();
            AnsiConsole.Clear();
            using (SQLiteDataReader students = Student.Model.StudentModel.GetAllStudent())
            {
                while (students.Read())
                {
                    char response;
                    do
                    {
                        response = AnsiConsole.Ask<char>($"L'étudiant {students["firstname"]} {students["lastname"]} est-il absent ou présent ? (a pour absent, p pour présent)");
                        switch (response)
                        {
                            case 'a':
                            case 'A':
                                absentStudents.Add(Convert.ToInt32(students["user_id"]));
                                AnsiConsole.MarkupLine("[red]Absent[/]");
                                break;
                            case 'p':
                            case 'P':
                                AnsiConsole.MarkupLine("[green]Présent[/]");
                                break;
                            default:
                                AnsiConsole.MarkupLine("[yellow]Erreur. Taper 'a' ou 'p'[/]");
                                break;
                        }
                    } while (response != 'a' && response != 'p' && response != 'A' && response != 'P');
                }
                Absent.Model.AbsentModel.addAbsent(date, absentStudents);
                AnsiConsole.Clear();
            }
        }
    }
}
