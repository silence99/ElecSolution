using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Business.CommonControl
{
	public static class TimeControl
	{
		public static string GenerateTimeStamp()
		{
			TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return Convert.ToInt64(ts.TotalMilliseconds).ToString();
		}

		public static long GetMillisecons(DateTime time)
		{
			return (long)(time - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
		}
	}
}
