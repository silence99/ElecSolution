﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Emergence.Common.Model
{

    public class Night
    {

        [JsonProperty("weather")]
        public string Weather { get; set; }

        [JsonProperty("templow")]
        public string Templow { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonProperty("winddirect")]
        public string Winddirect { get; set; }

        [JsonProperty("windpower")]
        public string Windpower { get; set; }
    }

}
