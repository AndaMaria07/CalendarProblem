# CalendarProblem
Given the calendar booked time of two people find all available time they can meet.

Need to install TimePeriod Library for .NET

My solution use the following implementation for finding all available time slots two peaople can meet:

1. The GetCalendar function takes a calendarName parameter and asks the user to enter booked time slots for the calendar until they enter "done".
It returns a list of string arrays where each array contains two elements representing the start and end time of a booked time slot.

2. The GetCalendarRange function takes a calendarName parameter and asks the user to enter the start and end time of the calendar in the format of "HH:mm".
It returns a string array with two elements representing the start and end time.

3. The GetMeetingTime function asks the user to enter the required meeting time in minutes and returns it as an integer.

4. The GetAvailableMeetingTimes function takes the calendars, their ranges, and the required meeting time as parameters.
It first converts the range strings to DateTime objects.

5. It then calculates the intersection of the two calendar ranges to determine the range of time periods that need to be checked for availability.

6. It creates a TimePeriodCollection object called workinghours that represents the range of time periods that need to be checked for availability.
In this case, it represents the intersection of the two calendar ranges.

7. It creates a TimePeriodCollection object called allappointments that represents all the booked time slots from both calendars.
It does this by iterating through each time slot in each calendar and creating a TimeRange object from its start and end time.
It then adds each TimeRange object to the allappointments collection.

8. It uses the TimePeriodCombiner class to combine the overlapping booked time slots into non-overlapping periods of time,
represented by an ITimePeriodCollection object called usersperiods.

9. It then creates a TimePeriodCollection object called gaps and uses the TimeGapCalculator class to calculate the gaps between the workinghours
and the usersperiods collections. It adds each gap to the gaps collection.

10. Finally, it iterates through each gap in the gaps collection and prints out the start and end times of each gap as a string in the format of "HH:mm".

11. In the Main function, it calls the various functions to get the required input from the user and then calls the GetAvailableMeetingTimes function 
to print out the available meeting times.

![image](https://user-images.githubusercontent.com/100118717/230486167-17307d47-9668-48e4-a699-35acdd67ddab.png)

