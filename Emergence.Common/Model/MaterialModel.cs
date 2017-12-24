using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class MaterialModel: NotificationObject
	{
		[JsonProperty("id")]
		public long ID { get; set; }
		[JsonProperty("bigMaterials")]
		public int IsBigMaterials { get; set; }
		[JsonProperty("consumables")]
		public int IsConsumable { get; set; }
		[JsonProperty("materialsDept")]
		public string MaterialsDept { get; set; }
		[JsonProperty("materialsName")]
		public string MaterialsName { get; set; }
		[JsonProperty("materialsNumber")]
		public string MaterialsNumber { get; set; }
		[JsonProperty("materialsType")]
		public string MaterialsType { get; set; }
		[JsonProperty("materialsTypeName")]
		public string MaterialsTypeName { get; set; }
		[JsonProperty("totalQuantity")]
		public int TotalQuantity { get; set; }
	}
}
