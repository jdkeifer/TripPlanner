using System;

namespace TripLibrary
{

	public static class ColorText
	{
		public static void HilightTextToGreen()
		{
			Console.ForegroundColor = ConsoleColor.Green;
		}
		public static void HilightTextToYellow()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
		}
		public static void HilightTextToRed()
		{
			Console.ForegroundColor = ConsoleColor.Red;
		}
		public static void HilightTextToDarkRed()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
		}
		public static void RevertTextColorToWhite()
		{
			Console.ForegroundColor = ConsoleColor.White;
		}

	}


}
