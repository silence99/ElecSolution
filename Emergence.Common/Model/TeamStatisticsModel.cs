using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class TeamStatisticsModel : NotificationObject
	{
		[JsonProperty("teamUseTotal")]
		public int TeamUseTotal { get; set; }
		[JsonProperty("teamMemberTotal")]
		public int TeamMemberTotal { get; set; }
		[JsonProperty("teamMemberUseTotal")]
		public int TeamMemberUseTotal { get; set; }
		[JsonProperty("teamTotal")]
		public int TeamTotal { get; set; }
	}
}
