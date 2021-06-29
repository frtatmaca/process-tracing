using System;
using System.Collections.Generic;
using System.Linq;

namespace SakaryaBel.Web
{
    public enum TimeStatus
    {
        Pass,
        Now,
        Future,
        None
    }
    public static class TimeHelper
    {
        public static TimeStatus RangeStatus(this DateTime? start, DateTime? end)
        {
            if (start.HasValue && end.HasValue)
                return RangeStatus(start.Value, end.Value);

            return TimeStatus.None;
        }
        public static TimeStatus RangeStatus(this DateTime start, DateTime end)
        {
            if (start > DateTime.Now)
                return TimeStatus.Future;
            if (start < DateTime.Now && end > DateTime.Now)
                return TimeStatus.Now;

            return TimeStatus.Pass;
        }

        public static string ToTRangeFormat(this DateTime? start, DateTime? end, string pass, string now, string future, string none = "")
        {
            var status = start.RangeStatus(end);
            switch (status)
            {
                case TimeStatus.Pass:
                    return pass;
                case TimeStatus.Now:
                    return now;
                case TimeStatus.Future:
                    return future;
            }

            return none;
        }
    }
    public static class FriendlyDate
    {

        public static string ToFDateTime(this object date, bool shour = false)
        {
            return Convert.ToDateTime(date).ToFDate(shour);
        }
        public static string ToFDate(this DateTime? date, bool shour = false)
        {
            return date.HasValue ? date.Value.ToFDate(shour) : "...";
        }

        public static string ToFDate(this DateTime date, bool shour = false)
        {
            string tx;
            if (DateTime.Now.Year != date.Year) // önceki yıl
                tx = date.ToString("MMMM yyyy");
            else if (DateTime.Now.WeekOfYear() != date.WeekOfYear()) // önceki hafta
            {
                tx = date.ToString("dd MMMM");
                if (shour)
                    tx = date.ToString("dd MMMM HH:mm");
            }
            else if (DateTime.Now.DayOfWeek != date.DayOfWeek) // önceki gün
                tx = date.ToString("dddd HH:mm");
            else if (DateTime.Now.DayOfWeek == date.DayOfWeek) // bugün
                tx = "Bugün " + date.ToString("HH:mm");
            else
                tx = date.ToString("dd.MM.yyyy HH:mm");
            return tx;
        }
        public static string ToTimeText(this TimeSpan ts)
        {
            if (ts.Days > 0)
                return ts.Days + " gün sonra";
            if (ts.Hours > 0)
                return ts.Hours + " saat sonra";
            if (ts.Minutes > 0)
                return ts.Hours + " dakika sonra";
            return "Başlamak üzere";
        }
        public static string ToFeDate(this DateTime sdate, DateTime edate, bool shour = false)
        {
            string tx;
            var ytx = (sdate.Year == DateTime.Now.Year ? "" : " " + sdate.Year);
            if (sdate.Year != edate.Year) // farklı yıllar
                tx = sdate.ToString("MMMM yyyy") + "-" + edate.ToString("MMMM yyyy"); // Ocak 2013 - Ocak 2014
            else if (sdate.Month != edate.Month) // farklı aylar
            {
                if (!shour)
                    tx = sdate.ToString("dd MMMM") + " - " + edate.ToString("dd MMMM") + ytx; // 12 Ocak - 10 Şubat (2014)
                else
                    tx = sdate.ToString("dd MMMM  HH:mm") + " - " + edate.ToString("dd MMMM  HH:mm") + ytx; // 12 Ocak 18:00 - 10 Şubat 20:00 (2014)
            }
            else if (sdate.Date != edate.Date) // aynı ay farklı gün
            {
                if (!shour)
                    tx = sdate.ToString("dd") + " - " + edate.ToString("dd MMMM") + ytx; // 10-13 Şubat (2014)
                else
                    tx = sdate.ToString("dd MMMM HH:mm") + " - " + edate.ToString("dd MMMM HH:mm") + ytx; // 10 Şubat@18:00 - 13 Şubat@20:00 (2014)
            }
            else if (sdate.Date == edate.Date) // aynı gün
                tx = sdate.ToString("dd MMMM HH:mm") + " - " + edate.ToString("HH:mm") + (ytx == "" ? "" : "(" + ytx + ")"); // 19 Şubat (2014) 18:00 - 22:00
            else
                tx = sdate.ToString("dd.MM.yyyy HH:mm") + " - " + edate.ToString("dd.MM.yyyy HH:mm");
            return tx;
        }


        public static string ToRDate(DateTime input)
        {
            var oSpan = DateTime.Now.Subtract(input);
            var totalMinutes = oSpan.TotalMinutes;
            var suffix = " önce";

            if (totalMinutes < 0.0)
            {
                totalMinutes = Math.Abs(totalMinutes);
                suffix = " şimdi";
            }

            var aValue = new SortedList<double, Func<string>>
            {
                {0.75, () => "less than a minute"},
                {1.5, () => "about a minute"},
                {45, () => string.Format("{0} minutes", Math.Round(totalMinutes))},
                {90, () => "about an hour"},
                {1440, () => string.Format("yaklaşık {0} saat", Math.Round(Math.Abs(oSpan.TotalHours)))},
                {2880, () => "bir gün"},
                {43200, () => string.Format("{0} gün", Math.Floor(Math.Abs(oSpan.TotalDays)))},
                {86400, () => "yaklaşık bir ay"},
                {525600, () => string.Format("{0} months", Math.Floor(Math.Abs(oSpan.TotalDays/30)))},
                {1051200, () => "yaklaşık bir yıl"},
                {double.MaxValue, () => string.Format("{0} yıl", Math.Floor(Math.Abs(oSpan.TotalDays/365)))}
            };
            // 60 * 24
            // 60 * 48
            // 60 * 24 * 30
            // 60 * 24 * 60
            // 60 * 24 * 365 
            // 60 * 24 * 365 * 2

            return aValue.First(n => totalMinutes < n.Key).Value.Invoke() + suffix;
        }




        private static int[] _moveByDays = { 6, 7, 8, 9, 10, 4, 5 };
        public static int WeekOfYear(this DateTime date)
        {
            DateTime startOfYear = new DateTime(date.Year, 1, 1);
            DateTime endOfYear = new DateTime(date.Year, 12, 31);
            // ISO 8601 weeks start with Monday 
            // The first week of a year includes the first Thursday 
            // This means that Jan 1st could be in week 51, 52, or 53 of the previous year...
            int numberDays = date.Subtract(startOfYear).Days +
                            _moveByDays[(int)startOfYear.DayOfWeek];
            int weekNumber = numberDays / 7;
            switch (weekNumber)
            {
                case 0:
                    // Before start of first week of this year - in last week of previous year
                    weekNumber = WeekOfYear(startOfYear.AddDays(-1));
                    break;
                case 53:
                    // In first week of next year.
                    if (endOfYear.DayOfWeek < DayOfWeek.Thursday)
                    {
                        weekNumber = 1;
                    }
                    break;
            }
            return weekNumber;
        }
    }
}
