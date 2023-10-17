using Spectre.Console;
using Repository;

namespace Controller
{
    public class LoginController
    {
        private const int MAX_ATTEMPTS = 3; 

        
        /// Valide les identifiants d'un utilisateur jusqu'à ce qu'il réussisse à se connecter
        /// ou dépasse le nombre maximal d'essais. Informe l'utilisateur de l'état de la connexion.
        public static void ValidationLogin()
        {
            bool isAuthenticated = false;
            int attempts = 0;

            while (!isAuthenticated && attempts < MAX_ATTEMPTS)
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
                    attempts++;
                    if(attempts == MAX_ATTEMPTS)
                    {
                        AnsiConsole.MarkupLine("[red]Trop de tentatives. Veuillez réessayer plus tard.[/]");
                        Environment.Exit(0);
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Mot de passe ou Identifiant invalide[/]");
                    }
                }
            }
        }
    }
}
