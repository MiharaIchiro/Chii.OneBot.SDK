using Chii.OneBot.SDK.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Params
{
    /// <summary>
    /// 處理加群請求／邀請
    /// </summary>
    internal class SetGroupAddRequestParams : IParams
    {
        /// <summary>
        /// 加群請求的 flag（需從上報的數據中獲得）
        /// </summary>
        [JsonPropertyName("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// add 或 invite，請求類型（需要和上報消息中的 sub_type 字段相符）
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// 是否同意請求／邀請
        /// </summary>
        [JsonPropertyName("approve")]
        public bool Approve { get; set; }

        /// <summary>
        /// 拒絕理由（僅在拒絕時有效）
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
