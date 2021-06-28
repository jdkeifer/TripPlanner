using System;
using System.Configuration;

namespace TripLibrary
{
	public static class ConfigureConsoleWindow
	{
		public static void SetWindowSizeAndLocation()
		{
			Console.WindowTop = 5;
			Console.WindowHeight = 55;
			Console.WindowWidth = 125;
		}
		public static void SetTheConsoleWindowTitle()
		{
			Console.Title = ConfigurationManager.AppSettings["ApplicationName"];
		}
		public static void ClearTheConsole()
		{
			Console.Clear();
		}
	}
}
