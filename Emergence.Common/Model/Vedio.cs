using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Emergence.Common.Model
{

    public class Video
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("videoLatitude")]
        public string VideoLatitude { get; set; }

        [JsonProperty("videoLocale")]
        public string VideoLocale { get; set; }

        [JsonProperty("videoLongitude")]
        public string VideoLongitude { get; set; }

        [JsonProperty("videoRemarks")]
        public string VideoRemarks { get; set; }

        [JsonProperty("videoTitle")]
        public string VideoTitle { get; set; }

        [JsonProperty("videoUrl")]
        public string VideoUrl { get; set; }
    }
}