using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class PersonModel
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
}
