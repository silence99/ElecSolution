using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class PersonModel : NotificationObject
	{
		public virtual bool IsChecked { get; set; }
		[JsonProperty("id")]
		public virtual long ID { get; set; }
		[JsonProperty("name")]
		public virtual string Name { get; set; }
		[JsonProperty("phoneNumber")]
		public virtual string PhoneNumber { get; set; }
		[JsonProperty("place")]
		public virtual string Place { get; set; }
		[JsonProperty("placeName")]
		public virtual string PlaceName { get; set; }
		[JsonProperty("teamId")]
		public virtual long TeamId { get; set; }
	}

	public class TeamMemberModel : NotificationObject
    {
        public virtual bool IsChecked { get; set; }
        [JsonProperty("phoneNumber")]
		public virtual string PhoneNumber { get; set; }
		[JsonProperty("place")]
		public virtual string Place { get; set; }
		[JsonProperty("placeName")]
		public virtual string PlaceName { get; set; }
		[JsonProperty("teamId")]
		public virtual string TeamId { get; set; }
		[JsonProperty("teamDept")]
		public virtual string TeamDept { get; set; }
		[JsonProperty("teamDeptName")]
		public virtual string TeamDeptName { get; set; }
		[JsonProperty("teamMemberId")]
		public virtual string TeamMemberId { get; set; }
		[JsonProperty("teamMemberName")]
		public virtual string TeamMemberName { get; set; }
		[JsonProperty("teamName")]
		public virtual string TeamName { get; set; }
	}
}
