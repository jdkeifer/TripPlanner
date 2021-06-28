using System;
using System.Configuration;

namespace TripLibrary.Models
{
	public class TripModel
	{

		private int _averageSpeed;

		public int AverageSpeed
		{
			get
			{
				return _averageSpeed;
			}
			set
			{
				_averageSpeed = Convert.ToInt32(ConfigurationManager.AppSettings["averageSpeed"]);
			}
		}

		private int _fuelTime;

		public int FuelTime
		{
			get
			{
				return _fuelTime;
			}
			set
			{
				_fuelTime = Convert.ToInt32(ConfigurationManager.AppSettings["fuelTime"]);
			}
		}


		public int MaxHours { get; set; } = 70;

		public int AvailableHours { get; set; }
		public int AvailableMinutes { get; set; }
		public int DriveTimeHours { get; set; }
		public int DriveTimeMinutes { get; set; }
		public int NeededHours { get; set; }
		public int NeededMinutes { get; set; }
		public int FuelStops { get; set; }
		public int TimeZoneOffset { get; set; }
		public int Miles { get; set; }
		public bool CanBeCompletedLegally { get; set; }

	}
}
