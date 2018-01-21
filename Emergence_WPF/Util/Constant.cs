namespace Emergence_WPF.Util
{
	public class Constant
	{
		public static ServicesNames Services = new ServicesNames();
		public static DefaultLocation DefaultLocation = new DefaultLocation() { Longutide = 119.133194, Latitude = 28.071823 };
	}

	public class ServicesNames
	{
		public string MasterEventService = "masterEventService";
		public string WeatherService = "weatherService";
		public string VideoStrategyController = "videoStrategyController";
		public string LoginService = "loginService";
		public string SubeventService = "subeventService";
		public string TeamStatisticsService = "teamStatisticsService";
	}

	public class DefaultLocation
	{
		public double Longutide { get; set; }
		public double Latitude { get; set; }
	}
}
