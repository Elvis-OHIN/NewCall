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
            Console.Write("\r\nPress Enter to return to Main Menu");
            Console.ReadLine();
        }
    }
}
