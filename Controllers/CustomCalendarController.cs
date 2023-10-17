using System.Globalization;
using Spectre.Console;
using Repository;
namespace Controller
{
     // Classe représentant un calendrier personnalisé avec la possibilité d'ajouter des événements.
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

            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_month);
            AnsiConsole.Write(new Rule($"[bold]{monthName} {_year}[/]").Centered());

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
                        row.Add(" ");  
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
                        currentDay++; 

                        if (currentDay <= daysInMonth)
                        {
                            currentDay++;
                        }
                    }
                }
            }
            var tables = new Table();
            
            tables.AddColumn("");
            tables.AddColumn("");
            tables.Border(TableBorder.None);
            tables.AddRow(table,new Markup("[bold cyan]Instructions du calendrier:[/]\n\n- Utilisez les flèches [bold]← →[/] pour naviguer jour par jour\n- Utilisez les flèches [bold]↑ ↓[/] pour naviguer semaine par semaine.\n- Les jours avec des absences sont marqués en [yellow]jaune[/]\n- Le jour sélectionné est en [red]rouge[/].\n- Appuyez sur [bold]Entrée[/] pour sélectionner un jour."));

            AnsiConsole.Write(tables);
        }
    }
}
