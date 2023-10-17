using Spectre.Console;

namespace Controller
{
    public class MenuController
    {
        // Affiche le menu principal à l'utilisateur et gère la sélection.
        public static bool MainMenu()
        {
            Console.Clear();
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Menu principal : Sélectionnez une option")
                    .PageSize(15)
                    .AddChoices(new[]
                    {
                        "Visualiser la liste des absences",
                        "Effectuer l'appel",
                        "Statistiques",
                        "Quitter"
                    }));

            switch (selection)
            {
                case "Visualiser la liste des absences":
                    AbsenceController.DisplayAbsenceListList();
                    return true;
                case "Effectuer l'appel":
                    StudentController.Call();
                    return true;
                case "Statistiques":
                    bool showMenu = true;
                    while (showMenu)
                    {
                        showMenu = StatsMenu();
                    }
                    return true;
                case "Quitter":
                    return false;
                default:
                    return true;
            }
        }

        // Affiche le menu des statistiques à l'utilisateur et gère la sélection.
        public static bool StatsMenu()
        {
            Console.Clear();
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Menu des statistiques : Sélectionnez une option")
                    .PageSize(15)
                    .AddChoices(new[]
                    {
                        "Statistiques globales",
                        "Statistiques par étudiant",
                        "Retour au menu principal"
                    }));

            switch (selection)
            {
                case "Statistiques globales":
                    AbsenceController.DisplayAbsencesGlobalStats();
                    return true;
                case "Statistiques par étudiant":
                    AbsenceController.DisplayAbsenceListListStats();
                    return true;
                case "Retour au menu principal":
                    return false;
                default:
                    return true;
            }
        }
    }
}
