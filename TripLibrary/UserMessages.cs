using System;

using TripLibrary.Models;

namespace TripLibrary
{
	public static class UserMessages
	{
		public static void RestBreakWarning(TripModel trip)
		{
			ColorText.HilightTextToDarkRed();
			Console.WriteLine("Warning!  Warning!  Warning!  Warning!\n");
			OnDutyTimeWarning();
			ColorText.HilightTextToGreen();
			Console.WriteLine("\n30 minute rest breaks can be conducted on duty, fueling etc...or they can be done off duty.");
			Console.WriteLine("This planner makes the assumption that all 30 minute rest breaks are done off duty.");
			Console.WriteLine($"If this is not the case you will need to subtract {trip.FuelTime} minutes for each rest break performed");
			Console.WriteLine("on duty.");
			ColorText.RevertTextColorToWhite();
		}
		public static void OnDutyTimeWarning()
		{
			ColorText.HilightTextToRed();
			Console.WriteLine("While every attempt has been made to ensure trip accuracy, \non-duty time at Shippers and Consignees must be considered when planning a trip");
			Console.WriteLine("Please allow an adequate amount of time to deal with backing in, \nsitting, waiting for paperwork, addressing OS&D issues etc....");
		}
		public static void TimeZoneMessage()
		{
			var defaultColor = Console.ForegroundColor;
			ColorText.HilightTextToGreen();
			Console.WriteLine("\nExample: \nfrom Central time zone, \nan input of -2 means you are traveling west through two timezones dropping two hours from your ETA \nan input of +2 means you are traveling east through two timezones and adding two hours to your ETA \n");
			ColorText.RevertTextColorToWhite();
		}
		public static void ThankyouMessage()
		{
			Console.Write("\nTrip planning completed, \n\nThank you for using Trip Planner to plan your trip...press [ENTER] to exit the application...");
		}
	}
}
