using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PeopleLocationApi.Helpers
{
    public static class JsonConvertorHelper
    {
        public static T DeserializeContent<T>(string content) where T : class
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy{}
                },
                Formatting = Formatting.Indented
            };

            return JsonConvert.DeserializeObject<T>(content, settings);
        }
    }
}
