using Framework;
using Newtonsoft.Json;
using System;

namespace Emergence.Common.Model
{
	public class ClientVersionModel : NotificationObject
	{
		[JsonProperty("id")]
		public virtual int ID { get; set; }
		[JsonProperty("url")]
		public virtual string Url { get; set; }
		[JsonProperty("version")]
		public virtual string Version { get; set; }
		[JsonProperty("createdTime")]
		public virtual DateTime CreatedTime { get; set; }
	}
}
