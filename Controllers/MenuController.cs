using Spectre.Console;

namespace Controller
{
    public class MenuController
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
                    AbsenceController.DisplayAbsentList();
                    return true;
                case "Faire l'appel":
                    StudentController.Call();
                    return true;
                case "Stats":
                    bool showMenu = true;
                    while (showMenu)
                    {
                        showMenu = StatsMenu();
                    }
                    return true;
                case "Quittez":
                    return false;
                default:
                    return true;
            }
        }
        public static bool StatsMenu()
        {
            Console.Clear();
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choisissez une option:")
                    .PageSize(15)
                    .AddChoices(new[]
                    {
                        "Stat global",
                        "Stat par étudiant",
                        "Quittez"
                    }));

            switch (selection)
            {
                case "Stat global":
                    AbsenceController.DisplayAbsencesGlobalStats();
                    return true;
                case "Stat par étudiant":
                    AbsenceController.DisplayAbsentListStats();
                    return true;
                case "Quittez":
                    return false;
                default:
                    return true;
            }
        }
    }
}
