using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EmployeeSpy.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(
                       obj,
                       Formatting.None,
                       new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
