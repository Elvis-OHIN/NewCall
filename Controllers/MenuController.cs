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
                    AbsentController.DisplayAbsentList();
                    return true;
                case "Faire l'appel":
                    StudentController.Call();
                    return true;
                case "Stats":
                    AbsentController.DisplayAbsentListStats();
                    return true;
                case "Quittez":
                    return false;
                default:
                    return true;
            }
        }
    }
}
