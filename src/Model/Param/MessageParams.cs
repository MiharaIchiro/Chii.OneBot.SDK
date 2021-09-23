using Chii.OneBot.SDK.Interface;
using Chii.OneBot.SDK.JsonConverter;
using Chii.OneBot.SDK.Model;
using Chii.OneBot.SDK.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Params
{
    /// <summary>
    /// 發送消息
    /// </summary>
    public class MessageParams : IParams
    {
        /// <summary>
        /// 消息類型，支持 private、group、discuss，分別對應私聊、群組、討論組，如不傳入，則根據傳入的 *_id 參數判斷
        /// </summary>
        [JsonPropertyName("message_type"), JsonConverter(typeof(EnumDescriptionConverterFactory))]
        public MessageFlags MessageType { get; set; }

        /// <summary>
        /// 對方 QQ 號（消息類型為 private 時需要）
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// 群號（消息類型為 group 時需要）
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 討論組 ID（消息類型為 discuss 時需要）
        /// </summary>
        [JsonPropertyName("discuss_id")]
        public long DiscussId { get; set; }

        /// <summary>
        /// 要發送的內容
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// 消息內容是否作為純文本發送（即不解析 CQ 碼）
        /// </summary>
        [JsonPropertyName("auto_escape")]
        public bool MessageAutoEscape { get; set; } = false;
    }
}
