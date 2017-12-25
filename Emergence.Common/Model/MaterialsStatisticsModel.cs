using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class MaterialsStatisticsModel : NotificationObject
	{
		[JsonProperty("totalQuantity")]
		public int TotalQuantity { get; set; }
		[JsonProperty("materialsName")]
		public string MaterialsName { get; set; }
	}
}
