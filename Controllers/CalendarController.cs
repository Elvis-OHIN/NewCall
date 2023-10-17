using System.Globalization;
using Spectre.Console;
using Repository;

namespace Controller
{
    // Permet à l'utilisateur de choisir un jour sur un calendrier interactif.
    public class CalendarController
    {
        public static DateTime ChooseDay()
        {
            AnsiConsole.Clear();
            var currentDate = DateTime.Now;
            var highlightedDate = currentDate;

            while (true)
            {
                Console.Clear(); 
            
                var customCalendar = new CustomCalendar(currentDate.Year, currentDate.Month);

                foreach(var dateCurrent in AbsenceListRepository.GetDateAbsenceList()){
                    customCalendar.AddEvent(new DateTime(dateCurrent.Year,dateCurrent.Month,dateCurrent.Day) , "yellow");
                }
                customCalendar.AddEvent(new DateTime(highlightedDate.Year,highlightedDate.Month,highlightedDate.Day), "red");

                customCalendar.Render();

                var key = Console.ReadKey(); 

                switch (key.Key)
                {

                    case ConsoleKey.LeftArrow:
                        DateTime nextDate = new DateTime(highlightedDate.Year, highlightedDate.Month, highlightedDate.Day);
                        if (nextDate.DayOfWeek == DayOfWeek.Monday)
                        {
                            highlightedDate = highlightedDate.AddDays(-3);
                        }else{
                            highlightedDate = highlightedDate.AddDays(-1);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        nextDate = new DateTime(highlightedDate.Year, highlightedDate.Month, highlightedDate.Day);
                        if (nextDate.DayOfWeek == DayOfWeek.Friday)
                        {
                            highlightedDate = highlightedDate.AddDays(3);
                        }else{
                            highlightedDate = highlightedDate.AddDays(1);
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        highlightedDate = highlightedDate.AddDays(-7);
                        break;

                    case ConsoleKey.DownArrow:
                        highlightedDate = highlightedDate.AddDays(7);
                        break;

                    case ConsoleKey.Enter:
                        return highlightedDate;
                }

                if (highlightedDate.Month != currentDate.Month)
                {
                    currentDate = new DateTime(highlightedDate.Year, highlightedDate.Month, 1);
                }
            }
        }
    }
}
