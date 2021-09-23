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
    ///  群組匿名用戶禁言
    /// </summary>
    internal class SetGroupAnonymousBanParams : IParams
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 要禁言的匿名用戶的 flag（需從群消息上報的數據中獲得）
        /// </summary>
        [JsonPropertyName("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// 禁言時長，單位秒，無法取消匿名用戶禁言
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }
    }
}
