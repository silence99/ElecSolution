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
        private bool isChecked = false;

        public bool IsChecked
        {
            get => isChecked; set
            {
                if (isChecked == value)
                {
                    return;
                }
                isChecked = value;
                RaisePropertyChangedEvent(new PropertyChangedEventArgsEx("IsChecked"));
            }
        }
        [JsonProperty("childEventState")]
        public string ChildEventState { get; set; }

        [JsonProperty("childEventStateName")]
        public string ChildEventStateName { get; set; }

        [JsonProperty("childEventType")]
        public string ChildEventType { get; set; }

        [JsonProperty("childEventTypeName")]
        public string ChildEventTypeName { get; set; }

        [JsonProperty("childGrade")]
        public string ChildGrade { get; set; }

        [JsonProperty("childGradeName")]
        public string ChildGradeName { get; set; }

        [JsonProperty("childLocale")]
        public string ChildLocale { get; set; }

        [JsonProperty("childRemarks")]
        public string ChildRemarks { get; set; }

        [JsonProperty("childSubmitDept")]
        public string ChildSubmitDept { get; set; }

        [JsonProperty("childSubmitParty")]
        public string ChildSubmitParty { get; set; }

        [JsonProperty("childSubmitTelephoneNumber")]
        public string ChildSubmitTelephoneNumber { get; set; }

        [JsonProperty("childTime")]
        public string ChildTime { get; set; }

        [JsonProperty("childTitle")]
        public string ChildTitle { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("mainEventId")]
        public int MainEventId { get; set; }
    }
}
