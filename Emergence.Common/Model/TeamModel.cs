using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class TeamModel : NotificationObject
	{
		[JsonProperty("id")]
		public long ID { get; set; }
		[JsonProperty("personCharge")]
		public string PersonCharge { get; set; }
		[JsonProperty("personChargePhone")]
		public string PersonChargePhone { get; set; }
		[JsonProperty("teamDept")]
		public string TeamDept { get; set; }
		[JsonProperty("teamName")]
		public string TeamName { get; set; }
		[JsonProperty("totalNumber")]
		public int TotalNumber { get; set; }
	}
}
