using Framework;
using Newtonsoft.Json;
using System;

namespace Emergence.Common.Model
{
	public class AnnouncementModel : NotificationObject
	{
		[JsonProperty("content")]
		public string Content { get; set; }
		[JsonProperty("id")]
		public int ID { get; set; }
		[JsonProperty("time")]
		public DateTime Time { get; set; }
		[JsonProperty("title")]
		public string Title { get; set; }
	}
}
