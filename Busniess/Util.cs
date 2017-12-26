using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
