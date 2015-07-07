public static class NonWorkingDays
{
    public static bool IsHoliday(DateTime day)
    {
        day.AssertArgumentNotNull("day");

        return
            NonWorkingDays
                .GetNonWorkingDays(day.Year)
                .Contains(day);
    }

    public static IList<DateTime> GetNonWorkingDays(int year)
    {
        IList<DateTime> days = new List<DateTime>();

        days.Add(new DateTime(year, 01, 01));
        days.Add(new DateTime(year, 01, 06));
        days.Add(new DateTime(year, 04, 25));
        days.Add(new DateTime(year, 05, 01));
        days.Add(new DateTime(year, 06, 02));
        days.Add(new DateTime(year, 06, 24));
        days.Add(new DateTime(year, 08, 15));
        days.Add(new DateTime(year, 10, 01));
        days.Add(new DateTime(year, 12, 08));
        days.Add(new DateTime(year, 12, 25));
        days.Add(new DateTime(year, 12, 26));
        days.Add(NonWorkingDays.EasterDay(year));
        days.Add(NonWorkingDays.EasterDay(year).AddDays(1));


        DateTime dummy = new DateTime(year, 1, 1);

        while (dummy.Year == year)
        {
            if (!days.Contains(dummy) && dummy.DayOfWeek == DayOfWeek.Saturday || dummy.DayOfWeek == DayOfWeek.Sunday)
            {
                days.Add(dummy);
            }

            dummy = dummy.AddDays(1);
        }

        return days;
    }

    public static DateTime EasterDay(int year)
    {
        int day = 0;
        int month = 0;

        int g = year % 19;
        int c = year / 100;
        int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
        int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

        day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
        month = 3;

        if (day > 31)
        {
            month++;
            day -= 31;
        }

        return new DateTime(year, month, day);
    }
}
