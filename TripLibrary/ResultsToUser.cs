using System;

using TripLibrary.Models;

namespace TripLibrary
{
	public class ResultsToUser
	{
		public static void ReturnResultsToUser(string message)
		{
			Console.WriteLine(message);
		}
		public static void DisplayResults(TripModel trip , DateTime start)
		{
			Console.Clear();

			// Pastel extension installed, but not fully implemented!
			//***** Console.WriteLine("Hello".Pastel(Color.Red)); ***** Pastel Extension!!


			int restBreakCount = trip.Miles / (trip.AverageSpeed * 8);
			int sleepBreakCount = trip.Miles / (trip.AverageSpeed * 11);

			int neededTime = ((restBreakCount * 30) + (sleepBreakCount * 600) + ((trip.DriveTimeHours * 60) + (trip.FuelStops * trip.FuelTime) + (trip.DriveTimeMinutes) /*+ (trip.TimeZoneOffset * 60)*/));

			Console.WriteLine("\n");
			Console.WriteLine($"with {trip.AvailableHours} hours and {trip.AvailableMinutes} minutes available, and {trip.Miles} miles to go averaging {trip.AverageSpeed} mph.....\n\n");
			Console.WriteLine("=========================================");
			Console.WriteLine($"driving time : {trip.DriveTimeHours} hours and {trip.DriveTimeMinutes} minutes. ");
			Console.WriteLine($"rest breaks  : {restBreakCount}  ({restBreakCount * 30} minutes) \nsleep breaks : {sleepBreakCount}  ({sleepBreakCount * 10} hours) \nfuel stops   : {trip.FuelStops}  ({trip.FuelStops * trip.FuelTime} minutes) \nTime Zones   : {trip.TimeZoneOffset} ({trip.TimeZoneOffset * 60} minutes)");
			Console.WriteLine("=========================================");
			Console.WriteLine($"{neededTime} minutes, or { neededTime / 60} hours and {neededTime % 60} minutes\n");

			int chargeableTime = (trip.DriveTimeHours * 60) + trip.DriveTimeMinutes + (trip.FuelStops * trip.FuelTime);
			double tempChargeTime = Convert.ToDouble(chargeableTime / 60.00);

			trip.NeededHours = chargeableTime / 60;
			trip.NeededMinutes = chargeableTime % 60;

			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.WriteLine($"DOT Chargeable time is {chargeableTime / 60} hours, {chargeableTime % 60 } minutes.");
			ColorText.RevertTextColorToWhite();

			DateTime newETA = start.AddMinutes(Convert.ToDouble(neededTime));

			Console.WriteLine("\nTime Zone Adjustment:");

			char[] charsToTrim = { '-' , '+' };
			string timeZoneTrimmed = trip.TimeZoneOffset.ToString();
			string result = timeZoneTrimmed.Trim(charsToTrim);

			TimeSpan timeZoneValue = new TimeSpan(0 , trip.TimeZoneOffset , 0 , 0 , 0);

			if (trip.TimeZoneOffset < 0)
			{
				Console.WriteLine($"Subtracting {result} hour(s) from your ETA: {newETA}");
				newETA = newETA.Add(timeZoneValue);
			}
			if (trip.TimeZoneOffset == 0)
			{
				Console.WriteLine("No ETA adjustment is required for time zones.");
			}
			if (trip.TimeZoneOffset > 0)
			{
				Console.WriteLine($"Adding {result} hour(s) to your ETA of {newETA}.");
				newETA = newETA.Add(timeZoneValue);
			}

			Console.WriteLine($"\nIf you start your trip { start.ToString("dddd, MMM d")} at {start.ToString(" HH:mm:ss")}, you will arrive { newETA.ToString("dddd, MMM d")} at {newETA.ToString("HH:mm:ss") } approximately..\n");

			int hoursCheck = (trip.AvailableHours * 60) + trip.AvailableMinutes;

			if (hoursCheck >= chargeableTime)
			{
				ColorText.HilightTextToGreen();
				Console.WriteLine($"You have enough time to legally complete this trip, good luck, and be safe.");
				ColorText.HilightTextToYellow();
				Console.WriteLine();
				Console.WriteLine("=================================================================================================");
				Console.WriteLine($"With this trip completed, you will have {((trip.AvailableHours * 60) + trip.AvailableMinutes - chargeableTime) / 60} hours and { ((trip.AvailableHours * 60) + trip.AvailableMinutes - chargeableTime) % 60 } minute(s) remaining.");
				Console.WriteLine("=================================================================================================\n");
				ColorText.RevertTextColorToWhite();
				trip.CanBeCompletedLegally = true;
			}
			else
			{
				ColorText.HilightTextToRed();
				Console.WriteLine("You don't appear to have enough time available to complete this trip,\nplease verify that your information for this trip was put into the system correctly.\n");
				ColorText.HilightTextToGreen();
				Console.WriteLine("*************************************************************");
				ColorText.HilightTextToYellow();
				Console.WriteLine($"Your available time is ({trip.AvailableHours}) hours and ({trip.AvailableMinutes}) minutes.");
				ColorText.HilightTextToGreen();
				Console.WriteLine("*************************************************************");
				ColorText.RevertTextColorToWhite();
				trip.CanBeCompletedLegally = false;

			}



		}
	}
}
