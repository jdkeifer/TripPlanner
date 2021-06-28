using System;
using System.Globalization;

using TripLibrary.Models;

namespace TripLibrary
{
	public class RequestData
	{
		public static void GetAvailableTime(TripModel trip)
		{
			Console.Clear();

			bool isValidInteger = false;
			bool isInsufficientTime = false;

			do
			{
				Console.Write("Enter your hours available: ");
				isValidInteger = Int32.TryParse(Console.ReadLine() , out int inputHours);
				trip.AvailableHours = inputHours;

				if (inputHours > trip.MaxHours)
				{
					ColorText.HilightTextToDarkRed();
					Console.WriteLine($"\nYou only get 70 hours in 8 days, not {trip.AvailableHours} hours!\n");
					ColorText.RevertTextColorToWhite();
					//Console.WriteLine("Setting available hours to 70 hours.\n");
					//trip.AvailableHours = trip.MaxHours;
					//trip.AvailableMinutes = 0;
				}
				if (inputHours < 0)
				{
					ColorText.HilightTextToDarkRed();
					Console.WriteLine($"Available hours can't be a negative number ({inputHours})");
					ColorText.RevertTextColorToWhite();
					isValidInteger = false;
				}
				else if (inputHours == 0)
				{
					ColorText.HilightTextToDarkRed();
					Console.WriteLine($"We can't do any trip planning with {inputHours} hours available.");
					isInsufficientTime = true;
					ColorText.RevertTextColorToWhite();
				}

			} while (isValidInteger == false || trip.AvailableHours > trip.MaxHours || isInsufficientTime == true);

			if (trip.AvailableHours < trip.MaxHours)
			{
				int maxMinutes = 59;
				int inputMinutes;

				do
				{
					Console.Write("Enter your minutes available: ");
					isValidInteger = Int32.TryParse(Console.ReadLine() , out inputMinutes);

					if (isValidInteger == true)
					{
						trip.AvailableMinutes = inputMinutes;
					}
					if (inputMinutes == 0)
					{
						trip.AvailableMinutes = 0;
						
					}
					if (trip.AvailableMinutes < 0 || trip.AvailableMinutes > 59)
					{
						ColorText.HilightTextToDarkRed();
						Console.WriteLine("minutes can't be a negative number or greater than 59!");
						ColorText.RevertTextColorToWhite();
					}


				} while (trip.AvailableMinutes > maxMinutes || isValidInteger == false || inputMinutes.ToString() == "" || trip.AvailableMinutes < 0);

			}

			Console.WriteLine($"Available time: {trip.AvailableHours} hours and {trip.AvailableMinutes} minutes");

		}
		public static void CalculateDriveTime(TripModel trip)
		{
			//Console.Clear();

			bool isValidMiles;

			double driveTime;
			double tempMinutes;

			do
			{
				string textMiles = GetConsoleInput("Enter your estimated miles for this trip: ");

				isValidMiles = int.TryParse(textMiles , out int miles);

				trip.Miles = miles;

				driveTime = miles / Convert.ToDouble(trip.AverageSpeed);
				var timeValues = driveTime.ToString("00.00" , CultureInfo.InvariantCulture).Split('.');
				trip.DriveTimeHours = int.Parse(timeValues[0]);
				tempMinutes = double.Parse(timeValues[1]) / 100;
				trip.DriveTimeMinutes = Convert.ToInt32(tempMinutes * 60.00);


				//Console.WriteLine("\nDrive time converted;\n");
				//Console.WriteLine($"Decimal minutes: { tempMinutes:.00} hours");
				//Console.WriteLine($"Hourly  minutes: {driveTimeMinutes} minutes");
				//Console.WriteLine();
				Console.WriteLine($"Decimal Representation:   Hours: {trip.DriveTimeHours}{tempMinutes:.00} hours drive time.");
				Console.WriteLine($"Hourly  Representation:   Hours: {trip.DriveTimeHours} hours and {trip.DriveTimeMinutes} minutes drive time.");

			} while (isValidMiles is false);


		}
		public static string GetConsoleInput(string messege)
		{
			Console.Write(messege);
			string output = Console.ReadLine();

			return output;
		}
		public static void CalculateFuelStops(TripModel trip)
		{

			bool isValidInteger = false;



			do
			{
				string textFuelStops = GetConsoleInput("\nHow many fuel stops will you be making?: ");

				isValidInteger = int.TryParse(textFuelStops , out int output);

				if (isValidInteger == true)
				{
					trip.FuelStops = output;
					trip.FuelTime = trip.FuelStops * output;

				}


			} while (isValidInteger == false);

		}
		public static void CalculateTimeZoneOffset(TripModel trip)
		{

			bool isValidInteger = false;


			do
			{
				//UserMessages.TimeZoneMessage();

				string textTimeZones = GetConsoleInput("How many time zones do you expect to cross?: ");
				isValidInteger = int.TryParse(textTimeZones , out int output);

				if (isValidInteger == true)
				{
					trip.TimeZoneOffset = output;

				}


			} while (isValidInteger == false);

		}
		public static void CalculateHoursAndMinutesFromDouble(double input)
		{
			int hours = Convert.ToInt32(input * 60.00);
			int minutes = Convert.ToInt32(input % 60.00);

			if (hours % 60 == 0)
			{
				Console.WriteLine($"Hours:{hours} and {minutes = 0}");
				Console.ReadLine();
			}
			else if (hours % 60 > 0)
			{
				Console.WriteLine($"Hours: {hours} and minutes: {minutes}");
				Console.ReadLine();

			}
		}
	}
}
