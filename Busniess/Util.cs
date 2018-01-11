using log4net;

namespace Busniess
{
	internal class Util
	{
		public static void ShowError(string message, ILog logger = null)
		{
			if (logger != null)
			{
				logger.Error(message);
			}
			// popup error window
		}
	}
}
