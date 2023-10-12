using Spectre.Console;
using Students;

namespace Menu.Controller
{
    public class Code
    {
        public static bool MainMenu()
        {
            Console.Clear();
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choisissez une option:")
                    .PageSize(15)
                    .AddChoices(new[]
                    {
                        "Voir la liste des absents",
                        "Faire l'appel",
                        "Stats",
                        "Quittez"
                    }));

            switch (selection)
            {
                case "Voir la liste des absents":
                    Absent.Controller.AbsentController.DisplayAbsentList();
                    return true;
                case "Faire l'appel":
                    Students.Controller.Code.Call();
                    return true;
                case "Stats":
                    Absent.Controller.AbsentController.DisplayAbsentListStats();
                    return true;
                case "Quittez":
                    return false;
                default:
                    return true;
            }
        }
    }
}
