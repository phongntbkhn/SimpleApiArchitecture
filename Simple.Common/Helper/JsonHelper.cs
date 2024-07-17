using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Helper
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static string Serialize<T>(IList<T> input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public static string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public static JObject ConvertStringToJson(this string input)
        {
            JObject json = JObject.Parse(input);
            return json;
        }
    }
}
