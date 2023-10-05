
using Students;
namespace Menu.Controller
{
    public class Code{
        public static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choisissez une option:");
            Console.WriteLine("1) Voir la liste des absents");
            Console.WriteLine("2) Faire l'appel");
            Console.WriteLine("3) Quittez");
            Console.Write("\r\nSélectionnez une option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Students.Controller.Code.ListAbsents();
                    return true;
                case "2":
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }
    }

}
