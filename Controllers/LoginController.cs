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
                Console.WriteLine($"Identifiant");
                // Lire ce que l'utilisateur a tapé
                identifiant = Console.ReadLine();
                Console.WriteLine($"Mot de passe");
                password = Console.ReadLine();
                if(identifiant == "admin" && password == "password"){
                    v = true;
                    Console.WriteLine($"Vous êtes connecté");
                }else{
                    Console.WriteLine($"Mot de passe ou Identifiant invalide");
                }
            } while (v != true);
    }
}

}
