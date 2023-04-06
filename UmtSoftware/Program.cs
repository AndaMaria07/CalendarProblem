using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MeetingScheduler
{
    class Program
    {
        static List<string[]> GetCalendar(string calendarName)
        {
            Console.Write($"Enter booked time for calendar {calendarName} (enter \"done\" when finished): ");
            List<string[]> calendar = new List<string[]>();
            while (true)
            {
                string input = Console.ReadLine().Trim();
                if (input.ToLower() == "done")
                {
                    break;
                }
                string[] timeSlot = input.Split(',');
                calendar.Add(timeSlot);
            }
            return calendar;
        }

        static string[] GetCalendarRange(string calendarName)
        {
            Console.Write($"Enter the range limits for calendar {calendarName} (start time,end time): ");
            string input = Console.ReadLine().Trim();
            return input.Split(',');
        }

        static int GetMeetingTime()
        {
            Console.Write("Enter the required meeting time in minutes: ");
            string input = Console.ReadLine().Trim();
            return int.Parse(input);
        }

        static void GetAvailableMeetingTimes(List<string[]> calendar1, string[] calendar1Range, List<string[]> calendar2, string[] calendar2Range, int meetingTimeMinutes)
        {
            DateTime calendar1Start = DateTime.Parse(calendar1Range[0]);
            DateTime calendar1End = DateTime.Parse(calendar1Range[1]);
            DateTime calendar2Start = DateTime.Parse(calendar2Range[0]);
            DateTime calendar2End = DateTime.Parse(calendar2Range[1]);


            var startRange = calendar1Start > calendar2Start ? calendar1Start : calendar2Start;
            var endRange = calendar1End < calendar2End ? calendar1End : calendar2End;

            TimePeriodCollection workinghours = new TimePeriodCollection();
            workinghours.Add(new TimeRange(startRange, endRange));

            TimePeriodCollection allappointments = new TimePeriodCollection();
            foreach (var booking in calendar1)
            {
                DateTime start = DateTime.Parse(booking[0]);
                DateTime end = DateTime.Parse(booking[1]);
                allappointments.Add(new TimeRange(start, end));

            }

            foreach (var booking in calendar2)
            {
                DateTime start = DateTime.Parse(booking[0]);
                DateTime end = DateTime.Parse(booking[1]);
                allappointments.Add(new TimeRange(start, end));

            }

            ITimePeriodCollection usersperiods = new TimePeriodCombiner<TimeRange>().CombinePeriods(allappointments);

            TimePeriodCollection gaps = new TimePeriodCollection();
            foreach (ITimePeriod basePeriod in workinghours)
            {
                gaps.AddAll(new TimeGapCalculator<TimeRange>().GetGaps(usersperiods, basePeriod));
            }

            foreach (ITimePeriod gap in gaps)
            {
                string startTimeString = gap.Start.ToString("HH:mm", CultureInfo.InvariantCulture);
                string endTimeString = gap.End.ToString("HH:mm", CultureInfo.InvariantCulture);
                string timePeriodString = $"{startTimeString} , {endTimeString}";

                Console.WriteLine(timePeriodString);
            }
        }


        static void Main(string[] args)
        {
            List<string[]> calendar1 = GetCalendar("1");
            string[] calendar1Range = GetCalendarRange("1");
            List<string[]> calendar2 = GetCalendar("2");
            string[] calendar2Range = GetCalendarRange("2");
            int meetingTimeMinutes = GetMeetingTime();
            Console.WriteLine("\nAvailable meeting times:");
            GetAvailableMeetingTimes(calendar1, calendar1Range, calendar2, calendar2Range, meetingTimeMinutes);
        }
    }

}
