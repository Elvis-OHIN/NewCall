using Spectre.Console;
using Repository;

namespace Controller
{
    public class LoginController
    {
        public static void ValidationLogin()
        {
            bool isAuthenticated = false;

            while (!isAuthenticated)
            {
                string identifiant = AnsiConsole.Prompt(
                    new TextPrompt<string>("Entrer votre [green]Identifiant[/]?"));
                string password = AnsiConsole.Prompt(
                    new TextPrompt<string>("Entrer votre [green]Mot de passe[/]?")
                        .Secret());

                if (AdminRepository.GetAdmin(identifiant, password))
                {
                    isAuthenticated = true;
                    AnsiConsole.MarkupLine("[green]Vous êtes connecté[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Mot de passe ou Identifiant invalide[/]");
                }
            }
        }
    }
}
