using Newtonsoft.Json;
using static Busniess.CommonControl.RequestControl;

namespace Busniess
{
	public class EmergencyHttpResponse<T> : EmergencyCommonResponse
	{
		[JsonProperty("result")]
		public T Result { get; set; }
	}

	public class EmergencyHttpListResult<T>
	{
		[JsonProperty("count")]
		public int Count { get; set; }

		[JsonProperty("data")]
		public T[] Data { get; set; }

		[JsonProperty("pageIndex")]
		public int PageIndex { get; set; }

		[JsonProperty("pageSize")]
		public int PageSize { get; set; }
	}
}
