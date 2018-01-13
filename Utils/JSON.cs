using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Utils
{
	public static class JSONHelper
	{
		private static JsonSerializerSettings settings = new JsonSerializerSettings()
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};
		public static T ConvertToObject<T>(string json)
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(json);
			}
			catch (System.Exception e)
			{
				return default(T);
			}

        }
        public static string ToJsonString(object obj)
        {
            try
            {
                if (obj != null)
                {
                    return JsonConvert.SerializeObject(obj);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (System.Exception e)
            {
                return string.Empty;
            }
        }
    }
}
