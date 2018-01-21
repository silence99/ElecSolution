using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
	public class SubEvent : NotificationObject
    {
        public virtual bool IsChecked { get; set; }
        [JsonProperty("id")]
        public virtual int Id { get; set; }
        [JsonProperty("childGrade")]
        public virtual string ChildGrade { get; set; }
        [JsonProperty("childGradeName")]
        public virtual string ChildGradeName { get; set; }
        [JsonProperty("childLatitude")]
        public virtual double ChildLatitude { get; set; }
        [JsonProperty("childLocale")]
        public virtual string ChildLocale { get; set; }
        [JsonProperty("childLongitude")]
        public virtual double ChildLongitude { get; set; }
        [JsonProperty("childRemarks")]
        public virtual string ChildRemarks { get; set; }
        [JsonProperty("childTitle")]
        public virtual string ChildTitle { get; set; }
        [JsonProperty("personLiable")]
        public virtual string PersonLiable { get; set; }
        [JsonProperty("state")]
        public virtual string State { get; set; }
        [JsonProperty("mainEventId")]
        public virtual string MainEventId { get; set; }

        public SubEvent()
        {
            IsChecked = false;
            ChildLongitude = 119.133194;
            ChildLatitude = 28.071823;
        }
    }
}
