using Spectre.Console;
using Students.Controller;

namespace Calendars.Controller
{
    public class CalendarController
    {
        public static DateTime ChooseDay()
        {
            AnsiConsole.Clear();
            var currentDate = DateTime.Now;
            var highlightedDate = currentDate;

            while (true)
            {
                Console.Clear(); // Efface la console

                AnsiConsole.Write(
                    new Calendar(currentDate.Year, currentDate.Month)
                        .HighlightStyle(Style.Parse("yellow"))
                        .Culture("fr")
                        .AddCalendarEvent(highlightedDate));

                var key = Console.ReadKey(); // Attend une entrée clavier

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        highlightedDate = highlightedDate.AddDays(-1);
                        break;

                    case ConsoleKey.RightArrow:
                        highlightedDate = highlightedDate.AddDays(1);
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
