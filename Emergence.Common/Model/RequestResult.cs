using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Model
{
    public class RequestResult
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
}
