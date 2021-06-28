using System;
using System.Globalization;

namespace TripLibrary
{
	public static class DateTimeHelper
	{
		public static DateTime GetTripStartDateAndTime()
		{
			bool result = false;
			DateTime dt = DateTime.Now;
			DateTime now = DateTime.Now;


			do
			{
				const DateTimeStyles style = DateTimeStyles.AllowWhiteSpaces;
				string[] dateFmt = { "M/d HH:mm" , "M/d hh:mm tt" , "M/d hh:mm" , "M/d h:mm tt" };
				string dtStr = "Format 1: 03/21 3:26 PM\nFormat 2: 3/21 03:26 AM\nFormat 3: 03/21 03:26 (24 hour time format)\nFormat 4: 3/21 15:26 (24 hour time format)";

				ColorText.HilightTextToGreen();
				Console.WriteLine("\nAcceptable Date/Time formats;\n");
				Console.WriteLine(dtStr);
				ColorText.RevertTextColorToWhite();
				Console.Write("\nEnter Trip start date and time in one of the above formats: ");
				dtStr = Console.ReadLine();

				result = DateTime.TryParseExact(dtStr , dateFmt , CultureInfo.InvariantCulture , style , out dt);

				if (result == true && dt <= now)
				{
					ColorText.HilightTextToYellow();
					Console.WriteLine($"DateTime value {dt} has already come and gone, please try again..");
					ColorText.RevertTextColorToWhite();


				}
				else if (result == false)
				{
					ColorText.HilightTextToRed();
					Console.WriteLine($"Invalid Date/Time format!! { dt }");
					ColorText.RevertTextColorToWhite();

				}
				else
				{
					return dt;
				}
			} while (result == false || dt <= now);

			return dt;

		}

	}
}
