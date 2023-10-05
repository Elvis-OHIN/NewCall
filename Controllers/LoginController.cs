using Students.Controller;


namespace Login.Controller
{
    public class Login {

        public static void Auth(){

            // Afficher la liste des étudiants à chaque étudiant.
            string? identifiant;
            string? password;
            bool? v = false;
            do
                {
                    Console.WriteLine("Identifiant");
                    identifiant = Console.ReadLine() ?? string.Empty;

                    Console.WriteLine("Mot de passe");
                    password = Console.ReadLine() ?? string.Empty;

                    if(Students.Controller.Code.GetUserByName(identifiant,password)){
                        v = true;
                        Console.WriteLine($"Vous êtes connecté");
                    }else{
                        Console.WriteLine($"Mot de passe ou Identifiant invalide");
                    }
                } while (v != true);
        }
    }
}
