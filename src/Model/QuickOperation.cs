using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model
{
    public sealed class QuickOperation
    {
        /// <summary>
        /// 回覆
        /// </summary>
        [JsonPropertyName("reply")]
        public string Reply { get; set; }

        /// <summary>
        /// @發送者
        /// </summary>
        [JsonPropertyName("at_sender")]
        public bool? AtSender { get; set; }

        /// <summary>
        /// 群組撤回成員消息
        /// </summary>
        [JsonPropertyName("delete")]
        public bool? Delete { get; set; }

        /// <summary>
        /// 群組踢人
        /// </summary>
        [JsonPropertyName("Kick")]
        public bool? Kick { get; set; }

        /// <summary>
        /// 群組禁言
        /// </summary>
        [JsonPropertyName("ban")]
        public bool? Ban { get; set; }

        /// <summary>
        /// 處理請求
        /// </summary>
        [JsonPropertyName("approve")]
        public bool? Approve { get; set; }

        /// <summary>
        /// 添加後的好友備註（僅在同意時有效）
        /// </summary>
        [JsonPropertyName("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 拒絕理由（僅在拒絕時有效）
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
