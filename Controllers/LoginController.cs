using Spectre.Console;
using Students.Controller;

namespace Login.Controller
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

                if (Admin.Model.AdminModel.GetAdmin(identifiant, password))
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
