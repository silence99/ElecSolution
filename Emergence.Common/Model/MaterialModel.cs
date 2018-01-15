using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class MaterialModel : NotificationObject
	{
		public virtual bool IsChecked { get; set; }
		[JsonProperty("id")]
		public virtual int ID { get; set; }
		[JsonProperty("bigMaterials")]
        public virtual string IsBigMaterials { get; set; }
        public virtual string IsBigMaterialsName { get; set; }
        [JsonProperty("consumables")]
        public virtual string IsConsumable { get; set; }
        public virtual string IsConsumableName { get; set; }
        [JsonProperty("materialsDept")]
		public virtual string MaterialsDept { get; set; }
		[JsonProperty("materialsDeptName")]
		public virtual string MaterialsDeptName { get; set; }
		[JsonProperty("materialsName")]
		public virtual string MaterialsName { get; set; }
		[JsonProperty("materialsNumber")]
		public virtual string MaterialsNumber { get; set; }
		[JsonProperty("materialsType")]
		public virtual string MaterialsType { get; set; }
		[JsonProperty("materialsTypeName")]
		public virtual string MaterialsTypeName { get; set; }
		[JsonProperty("totalQuantity")]
		public virtual int TotalQuantity { get; set; }
		public MaterialModel()
		{
			IsChecked = false;
		}

		public virtual bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(MaterialsName);
			}
		}
	}
}
