using System.Windows;

namespace Business.Services
{
	public static class ResolutionService
	{
		public static double Width { get; set; }
		public static double Height { get; set; }

		static ResolutionService()
		{
			Width = 1920;
			Height = 1080;
		}

		public static Point GetCenterControlOffset(double parentWidth, double parentHeight, double controlWidth, double controlHeight)
		{
			return new Point((parentWidth - controlWidth) / 2, (parentHeight - controlHeight) / 2);
		}

		public static Point GetCenterControlOffset(double controlWidth, double controlHeight)
		{
			return new Point((Width - controlWidth) / 2, (Height - controlHeight) / 2);
		}
	}
}
