using System;
using System.Collections.Generic;
using System.Configuration;

using TripLibrary;
using TripLibrary.Models;

namespace ConsoleUI
{
	public class Program
	{
		static void Main(string[] args)
		{
			ConfigureConsoleWindow.SetWindowSizeAndLocation();
			ConfigureConsoleWindow.SetTheConsoleWindowTitle();
			ConfigureConsoleWindow.ClearTheConsole();

			List<TripModel> trips = new List<TripModel>();

			bool moreTrips = false;

			TripModel trip = new TripModel();

			do
			{


				trip.AverageSpeed = Convert.ToInt32(ConfigurationManager.AppSettings["averageSpeed"]);
				trip.FuelTime = Convert.ToInt32(ConfigurationManager.AppSettings["fuelTime"]);

				AppSettings.GetCurrentSettings(); // verify the user want to use these settings and if not, change them.


				DateTime ETA = DateTimeHelper.GetTripStartDateAndTime(); // Get a valid DateTime group (DTG) in the required format that is now or in the future.


				RequestData.GetAvailableTime(trip); // Get the available time left on the user's DOT 70 hour clock.
				RequestData.CalculateDriveTime(trip); // Get trip miles and Calculate how much drive time is required to drive the requested miles.
				RequestData.CalculateFuelStops(trip); // Get the number of times the user expects to stop for fuel during this trip.

				UserMessages.TimeZoneMessage(); // informational message to user for proper time zone input.

				RequestData.CalculateTimeZoneOffset(trip); // Determines whether to add, subtract, or do nothing to the ETA to compensate for time zones.

				UserMessages.RestBreakWarning(trip); // information on conducting rest breaks "on" or "off" duty and the possible impact to available time.

				ResultsToUser.DisplayResults(trip , (DateTime)ETA); // Calculates and displays current information about current trip and trip requirements.

				Console.WriteLine();

				//foreach (PropertyInfo prop in typeof(TripModel).GetProperties())
				//{
				//	Console.WriteLine("{0} = {1}" , prop.Name , prop.GetValue(trip , null));
				//}
				Console.ReadLine();

				trips.Add(trip);

				string input = "";
				bool isValidInput = false;

				while (isValidInput == false)
				{
					Console.Write("Do you want to run another scenario through Trip Planner? ('y' or 'n'): ");
					input = Console.ReadLine();

					if (input.ToLower() == "y")
					{
						isValidInput = true;
						//continue;
					}
					if (input.ToLower() == "n")
					{
						isValidInput = true;
						//continue;
					}
					if (input.ToLower() == "")
					{
						isValidInput = false;
					}

				} // END while isValidInput

				if (input.ToLower() == "n")
				{
					Console.WriteLine("Results displayed for your trip scenario(s)...\n");
					foreach (TripModel listtrip in trips)
					{
						Console.WriteLine($"Planning speed:{trip.AverageSpeed} / Fuel time:{trip.FuelTime} / Miles:{listtrip.Miles} / Time Avail.:{listtrip.AvailableHours}:{listtrip.AvailableMinutes} / Time Needed: {trip.NeededHours}:{trip.NeededMinutes} / I am able to Run this trip: {listtrip.CanBeCompletedLegally}");
					}
					moreTrips = false;
				}

				else if (input.ToLower() == "y")
				{
					moreTrips = true;
					trip = new TripModel();
				}



			} while (moreTrips == true);

			Console.ReadLine();

			UserMessages.ThankyouMessage();

			Console.ReadLine();

		}

	}

}