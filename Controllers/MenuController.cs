
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
            Console.WriteLine("3) Stats");
            Console.WriteLine("4) Quittez");
            Console.Write("\r\nSélectionnez une option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Absent.Controller.AbsentController.DisplayAbsentList();
                    return true;
                case "2":
                    Students.Controller.Code.Call();
                    return true;
                case "3":
                    Absent.Controller.AbsentController.DisplayAbsentListStats();
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }
        }
    }

}
