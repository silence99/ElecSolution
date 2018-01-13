using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class CameraModel : NotificationObject
	{
		[JsonProperty("dept")]
		public virtual string Dept { get; set; }
		[JsonProperty("distance")]
		public virtual double Distance { get; set; }
		[JsonProperty("id")]
		public virtual long ID { get; set; }
		[JsonProperty("latitude")]
		public virtual double Latitude { get; set; }
		[JsonProperty("longitude")]
		public virtual double Longitude { get; set; }
		[JsonProperty("locale")]
		public virtual string Locale { get; set; }
		[JsonProperty("manufacturer")]
		public virtual string Manufacturer { get; set; }
		[JsonProperty("playAttribute")]
		public virtual string PlayAttribute { get; set; }
		[JsonProperty("playMode")]
		public virtual string PlayMode { get; set; }
		[JsonProperty("remarks")]
		public virtual string Remarks { get; set; }
		[JsonProperty("system")]
		public virtual string System { get; set; }
		[JsonProperty("url")]
		public virtual string Url { get; set; }
		[JsonProperty("videoNumber")]
		public virtual string VideoNumber { get; set; }

	}
}
