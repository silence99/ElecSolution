using Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Model
{
    public class SummaryEvaluationModel : NotificationObject
    {
        [JsonProperty("id")]
        public virtual long Id { get; set; }
        [JsonProperty("impleAssessment")]
        public virtual string ImpleAssessment { get; set; }
        [JsonProperty("mainEventId")]
        public virtual long MainEventId { get; set; }
        [JsonProperty("organAssessment")]
        public virtual string OrganAssessment { get; set; }
        [JsonProperty("summary")]
        public virtual string Summary { get; set; }
        [JsonProperty("type")]
        public virtual int Type { get; set; }
    }
}
