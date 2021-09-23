using Chii.OneBot.SDK.Enumeration;
using Chii.OneBot.SDK.Interface;
using Chii.OneBot.SDK.JsonConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model
{
    public class CqHttpRequest
    {
        [JsonPropertyName("action"), JsonConverter(typeof(EnumDescriptionConverterFactory))]
        public RequestType Task { get; set; }

        [JsonPropertyName("echo")]
        public string Echo { get; set; }

        [JsonPropertyName("params"), JsonConverter(typeof(ParamConverter))]
        public IParams Params { get; set; }
    }
}
