﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Emergence.Common.Model
{

    public class Hourly
    {

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("weather")]
        public string Weather { get; set; }

        [JsonProperty("temp")]
        public string Temp { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }
    }

}
