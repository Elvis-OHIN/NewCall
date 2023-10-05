namespace Students.Controller
{
    public class Code{
        public static void ListAbsents(){
            Console.Clear();
            List<Student> absents = new List<Student>();
            Console.WriteLine($"Liste des absents");
            foreach (Student currentAbsents in absents) {
                Console.WriteLine($"{currentAbsents.LastName}  {currentAbsents.Firstname}");
            }
            Console.Write("\r\nAppuyez sur Entrée pour retourner au menu principal");
            Console.ReadLine();
        }

        public static void Call(){
            Console.Clear();
            String? s;
            //Afficher la liste des étudiants à chaque étudiant.
            List<Student> students = new List<Student>(){};
            List<Student> absents = new List<Student>();
            students.Add(new Student("CHABEAUDIE" , "Maxime"));
            students.Add(new Student("FETTER" , "Léo"));
            students.Add(new Student("MONTASTIER" , " Florian"));
            students.Add(new Student("MARTIN" , "Jérémy"));
            students.Add(new Student("OHIN" , "ELvis"));
            foreach (Student studentCurrent in students) {
                do {
                    Console.WriteLine($"L'étudiant {studentCurrent.LastName} {studentCurrent.Firstname} est-il absent ou présent ? Tapez 'a' pour absent ou 'p' pour présent");

                    //Lire ce que l'utilisateur a tapé
                    s = Console.ReadLine();
                    switch (s)   {
                        //Ajouter l'étudiant à la liste des absents
                        case "a" or "A":
                            absents.Add(studentCurrent);
                            s = "ok";
                            Console.WriteLine($"Absent");
                            break;

                        case "p" or "P":
                            s = "ok";
                            Console.WriteLine($"Présent");
                            break;
                        //Si c'est incorrect, afficher message d'erreur
                        default:
                            Console.WriteLine($"Erreur. Taper 'a' ou 'p'");
                            break;
                        }
                    } while (s != "ok");
            }
            Console.Write("\r\nAppuyez sur Entrée pour retourner au menu principal");
            Console.ReadLine();
        }
    }
}
