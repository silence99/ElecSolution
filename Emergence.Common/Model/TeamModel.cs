using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class TeamModel : NotificationObject
	{
		public virtual bool IsChecked { get; set; }
		[JsonProperty("id")]
		public virtual string ID { get; set; }

        [JsonProperty("teamMemberId")]
        public virtual long TeamMemberId { get; set; }
        [JsonProperty("personCharge")]
        public virtual string PersonCharge { get; set; }
        [JsonProperty("personChargePhone")]
		public virtual string PersonChargePhone { get; set; }
		[JsonProperty("teamDept")]
		public virtual string TeamDept { get; set; }
		[JsonProperty("teamDeptName")]
		public virtual string TeamDeptName { get; set; }
		[JsonProperty("teamDeptLocale")]
		public virtual string TeamLocale { get; set; }
		[JsonProperty("teamName")]
		public virtual string TeamName { get; set; }
		[JsonProperty("totalNumber")]
		public virtual int TotalNumber { get; set; }
        public TeamModel()
        {
        }
    }
}
