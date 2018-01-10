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
    }
}
