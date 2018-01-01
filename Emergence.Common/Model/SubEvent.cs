using Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Model
{
    public class SubEvent : NotificationObject
    {
        public virtual bool IsChecked { get; set; }
        [JsonProperty("id")]
        public virtual long Id { get; set; }
        [JsonProperty("childGrade")]
        public virtual string ChildGrade { get; set; }
        [JsonProperty("childGradeName")]
        public virtual string ChildGradeName { get; set; }
        [JsonProperty("childLatitude")]
        public virtual string ChildLatitude { get; set; }
        [JsonProperty("childLocale")]
        public virtual string ChildLocale { get; set; }
        [JsonProperty("childLongitude")]
        public virtual string ChildLongitude { get; set; }
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
        }
    }
}
