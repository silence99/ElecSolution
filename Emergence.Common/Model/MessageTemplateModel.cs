using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class MessageTemplateModel : NotificationObject
	{
		[JsonProperty("templateId")]
		public virtual string TemplateId { get; set; }
		[JsonProperty("templateContent")]
		public virtual string TemplateContent { get; set; }
		[JsonProperty("templateName")]
		public virtual string TemplateName { get; set; }
		[JsonProperty("templateType")]
		public virtual string TemplateType { get; set; }
	}
}
