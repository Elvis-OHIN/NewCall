using System;

namespace Absent.Controller
{
    public class AbsentController
    {
        // Affiche la liste des absents à l'écran.
        public static void DisplayAbsentList()
        {
            try
            {
                DateTime date = ChoiceDay();
                Console.Clear();
                Console.WriteLine("Liste des absents");

                // Récupère et affiche la liste des absents depuis le modèle.
                Model.AbsentModel.FetchAbsentsByDate(date);

                Console.Write("\r\nAppuyez sur Entrée pour retourner au menu principal");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur est survenue : {ex.Message}");
            }
        }

        public static DateTime ChoiceDay()
        {
            Console.Clear();
            string s;
            bool validDate;
            do
            {
                Console.WriteLine($"Veuillez entrer une date pour l'appel (format JJ/MM/AAAA) :");
                s = Console.ReadLine();
                validDate = IsValidDate(s, out DateTime parsedDate);
                if (validDate)
                {
                    return parsedDate;
                }
            } while (!validDate);

            return DateTime.MinValue;
        }

        public static bool IsValidDate(string date, out DateTime parsedDate)
        {
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Date non valide. Assurez-vous d'utiliser le format JJ/MM/AAAA");
                return false;
            }
        }
    }
}
