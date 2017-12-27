using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
    public class MasterEvent : NotificationObject
    {
        private bool isChecked = false;
        
        public virtual bool IsChecked { get => isChecked;
			set
            {
                if(isChecked == value)
                {
                    return;
                }
                isChecked = value;
            }
        }

        [JsonProperty("eventType")]
        public virtual string EventType { get; set; }

        [JsonProperty("eventTypeName")]
        public virtual string EventTypeName { get; set; }

        [JsonProperty("grade")]
        public virtual string Grade { get; set; }

        [JsonProperty("gradeName")]
        public virtual string GradeName { get; set; }

        [JsonProperty("id")]
        public virtual int ID { get; set; }

        [JsonProperty("latitude")]
        public virtual string Latitude { get; set; }

        [JsonProperty("locale")]
        public virtual string Locale { get; set; }

        [JsonProperty("longitude")]
        public virtual string Longitude { get; set; }

        [JsonProperty("remarks")]
        public virtual string Remarks { get; set; }

        [JsonProperty("serialNumber")]
        public virtual string SerialNumber { get; set; }

        [JsonProperty("submitDept")]
        public virtual string SubmitDept { get; set; }

        [JsonProperty("submitParty")]
        public virtual string SubmitParty { get; set; }

        [JsonProperty("telephoneNumber")]
        public virtual string TelephoneNumber { get; set; }

        [JsonProperty("time")]
        public virtual string Time { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("videos")]
        public virtual Video[] Videos { get; set; }
    }
}
