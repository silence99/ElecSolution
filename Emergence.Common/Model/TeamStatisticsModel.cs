using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class TeamStatisticsModel : NotificationObject
	{
		[JsonProperty("teamUseTotal")]
		public virtual int TeamUseTotal { get; set; }
		[JsonProperty("teamMemberTotal")]
		public virtual int TeamMemberTotal { get; set; }
		[JsonProperty("teamMemberUseTotal")]
		public virtual int TeamMemberUseTotal { get; set; }
		[JsonProperty("teamTotal")]
		public virtual int TeamTotal { get; set; }
	}
}
