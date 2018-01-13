using Newtonsoft.Json;
using System;

namespace Emergence.Common.Model
{
	public class MessageModel : MessageTemplateModel
	{
		[JsonProperty("childEventId")]
		public virtual long ChildEventId { get; set; }
		[JsonProperty("childTitle")]
		public virtual string childTitle { get; set; }
		[JsonProperty("createdTime")]
		public virtual DateTime CreatedTime { get; set; }
		[JsonProperty("templateTypeName")]
		public virtual string TemplateTypeName { get; set; }
	}
}
