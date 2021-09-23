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
    /// 處理加好友請求
    /// </summary>
    internal class SetFriendAddRequestParams : IParams
    {
        /// <summary>
        /// 加好友請求的 flag（需從上報的數據中獲得）
        /// </summary>
        [JsonPropertyName("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// 是否同意請求
        /// </summary>
        [JsonPropertyName("approve")]
        public bool Approve { get; set; }

        /// <summary>
        /// 添加後的好友備註（僅在同意時有效）
        /// </summary>
        [JsonPropertyName("remark")]
        public string Remark { get; set; }
    }
}
