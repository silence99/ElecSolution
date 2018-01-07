using Framework;
using Newtonsoft.Json;
using System;

namespace Emergence.Common.Model
{
	public class AnnouncementModel : NotificationObject
	{
		public virtual bool IsChecked { get; set; }
		[JsonProperty("content")]
		public virtual string Content { get; set; }
		[JsonProperty("id")]
		public virtual long ID { get; set; }
		[JsonProperty("time")]
		public virtual DateTime Time { get; set; }
		[JsonProperty("title")]
		public virtual string Title { get; set; }
	}
}
