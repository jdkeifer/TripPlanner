using System;
using System.Configuration;
using System.Xml;

namespace TripLibrary
{
	public static class AppSettings
	{
		public static int averageSpeed { get; set; }
		public static int fuelTime { get; set; }


		public static void GetCurrentSettings()
		{
			Console.Clear();

			string changeSettings;
			string speed;
			string fuel;

			do
			{
				speed = ReadAppSettingsSectionKey("averageSpeed");
				fuel = ReadAppSettingsSectionKey("fuelTime");

				Console.WriteLine($"\nPlanning speed: { speed } mph ");
				Console.WriteLine($"Time for each fuel stop is: { fuel } minutes. \n");
				Console.Write("Would you like to change these settings? (y/n): ");
				changeSettings = Console.ReadLine();

				if (changeSettings.ToLower() == "n")
				{
					speed = ReadAppSettingsSectionKey("averageSpeed");
					fuel = ReadAppSettingsSectionKey("fuelTime");

					Console.WriteLine($"\nPlanning speed: { speed } mph ");
					Console.WriteLine($"Time for each fuel stop is: { fuel } minutes. \n");

					return;
				}
				if (changeSettings.ToLower() == "y")
				{
					Console.Write("Enter the desired planning speed : ");
					string input = Console.ReadLine();
					UpdateAppSettingsSectionKey("averageSpeed" , input);
					averageSpeed = Convert.ToInt32(ConfigurationManager.AppSettings["averageSpeed"]);

					Console.Write("Enter the desired time (in minutes) for each fuel stop: ");
					input = Console.ReadLine();
					UpdateAppSettingsSectionKey("fuelTime" , input);
					fuelTime = Convert.ToInt32(ConfigurationManager.AppSettings["fuelTime"]);


					Console.WriteLine($"Current Planning speed: {averageSpeed} MPH.");
					Console.WriteLine($"Current time for each fuel stop: {fuelTime} minutes");
					Console.Write("\nPress [ENTER] to continue....");
					Console.ReadLine();
				}

			} while (changeSettings == "");


		}

		public static string ReadAppSettingsSectionKey(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		public static void UpdateAppSettingsSectionKey(string key , string value)
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			configuration.AppSettings.Settings[key].Value = value;
			configuration.Save(ConfigurationSaveMode.Modified , true);
			ConfigurationManager.RefreshSection("appSettings");

		}


		public static void UpdateOtherSection(string tagName , string attributeName , string value)
		{
			var doc = new XmlDocument();
			doc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
			var tags = doc.GetElementsByTagName(tagName);

			foreach (XmlNode item in tags)
			{
				var attribute = item.Attributes[attributeName];

				if (!ReferenceEquals(null , attribute))
				{
					attribute.Value = value;
				}
			}

			doc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
		}

	}
}

