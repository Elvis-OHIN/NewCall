using System.Globalization;
using Spectre.Console;
using Repository;
namespace Controller
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

                var customCalendar = new CustomCalendar(currentDate.Year, currentDate.Month);

                foreach(var dateCurrent in AbsenceListRepository.GetDateAbsenceList()){
                    customCalendar.AddEvent(new DateTime(dateCurrent.Year,dateCurrent.Month,dateCurrent.Day) , "yellow");
                }
                customCalendar.AddEvent(new DateTime(highlightedDate.Year,highlightedDate.Month,highlightedDate.Day), "red");

                customCalendar.Render();

                var key = Console.ReadKey(); // Attend une entrée clavier

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
        public class CustomCalendar
        {
        private readonly int _year;
        private readonly int _month;
        private readonly Dictionary<DateTime, string> _events = new();

        public CustomCalendar(int year, int month)
        {
            _year = year;
            _month = month;
        }

        public CustomCalendar AddEvent(DateTime date, string color)
        {
            _events[date] = color;
            return this;
        }

        public void Render()
        {
            var table = new Table();

            // Get month name and display it centered above the table
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_month);
            AnsiConsole.Write(new Rule($"[bold]{monthName} {_year}[/]").Centered());

            // Add headers (Day names)
            string[] AbbreviatedDayNames = new String[] { "Lun", "Mar", "Mer", "Jeu",
                                                "Ven" };
            foreach (var day in AbbreviatedDayNames)
            {
                table.AddColumn(new TableColumn(day).Centered());
            }

            DateTime firstDayOfMonth = new DateTime(_year, _month, 1);
            int daysInMonth = DateTime.DaysInMonth(_year, _month);
            int offset = (int)firstDayOfMonth.DayOfWeek - 1;
            if (offset == 6)
            {
                offset = 5;
            }
            else if (offset == 5)
            {
                offset = 4;
            }
            if (offset < 0) offset = 4;

            int currentDay = 1;
            while (currentDay <= daysInMonth)
            {
                var row = new List<string>();

                for (int dayOfWeek = 0; dayOfWeek < 5; dayOfWeek++)
                {
                    if ((dayOfWeek < offset && currentDay == 1) || currentDay > daysInMonth)
                    {
                        row.Add(" ");  // Empty cell
                    }
                    else
                    {
                        var currentDate = new DateTime(_year, _month, currentDay);
                        if (_events.TryGetValue(currentDate, out var color))
                        {
                            row.Add($"[{color}]{currentDay}[/]");
                        }
                        else
                        {
                            row.Add(currentDay.ToString());
                        }
                        currentDay++;
                    }
                }

                table.AddRow(row.ToArray());
                if (currentDay <= daysInMonth)
                {
                    DateTime nextDate = new DateTime(_year, _month, currentDay);
                    if (nextDate.DayOfWeek == DayOfWeek.Saturday)
                    {
                        currentDay++; // Saute le samedi

                        if (currentDay <= daysInMonth)
                        {
                            currentDay++; // Saute le dimanche si on n'est pas à la fin du mois
                        }
                    }
                }
            }

            AnsiConsole.Write(table);
        }
    }
}
