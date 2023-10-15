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
            List<int> absentStudents = new List<int>();
            DateTime date = CalendarController.ChooseDay();
            AnsiConsole.Clear();
            
            List<Student> students = StudentRepository.GetAllStudent();
            foreach (var student in students)
            {
                char response;
                do
                {
                    response = AnsiConsole.Ask<char>($"L'étudiant {student.Firstname} {student.Lastname} est-il absent ou présent ? (a pour absent, p pour présent)");
                    switch (response)
                    {
                        case 'a':
                        case 'A':
                            absentStudents.Add(student.Id);
                            AbsencesRepository.AddAbsence(student.Id,date.Date,date.Date,"","");
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

            var absent = AbsentRepository.GetAbsentListByDate(date.Date);
            if (!absent.HasRows)
            {
                AbsentRepository.addAbsent(date, absentStudents);
            }
            else
            {
                AbsentRepository.UpdateAbsent(date, absentStudents);
            }
            AnsiConsole.Clear();
        }
    }
}
