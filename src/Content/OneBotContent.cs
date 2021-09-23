using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Content
{
    public class OneBotContent
    {
        /// <summary>
        /// 操作結果返回碼
        /// </summary>
        [JsonPropertyName("retcode")]
        public int RetCode { get; set; }

        /// <summary>
        /// 結果狀態
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// 終端ID
        /// </summary>
        [JsonPropertyName("seifid")]
        public long SelfId { get; set; }

        /// <summary>
        /// 操作編號
        /// </summary>
        [JsonPropertyName("echo")]
        public string Echo { get; set; }

        /// <summary>
        /// 操作結果
        /// </summary>
        [JsonPropertyName("data")]
        public JsonElement Data { get; set; }
    }

}
