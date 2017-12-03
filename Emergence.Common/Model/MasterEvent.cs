using Framework;
using Newtonsoft.Json;

namespace Emergence.Common.Model
{
    public class MasterEvent : NotificationObject
    {
        private bool isChecked = false;
        
        public bool IsChecked { get => isChecked; set
            {
                if(isChecked == value)
                {
                    return;
                }
                isChecked = value;
                RaisePropertyChangedEvent(new PropertyChangedEventArgsEx("IsChecked"));
            }
        }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("eventTypeName")]
        public string EventTypeName { get; set; }

        [JsonProperty("grade")]
        public string Grade { get; set; }

        [JsonProperty("gradeName")]
        public string GradeName { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonProperty("submitDept")]
        public string SubmitDept { get; set; }

        [JsonProperty("submitParty")]
        public string SubmitParty { get; set; }

        [JsonProperty("telephoneNumber")]
        public string TelephoneNumber { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("videos")]
        public Video[] Videos { get; set; }
    }
}
