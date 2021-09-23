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
    /// 群組單人禁言
    /// </summary>
    internal class SetGroupBanParams : IParams
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 要禁言的 QQ 號
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// 禁言時長，單位秒，0 表示取消禁言
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }
    }
}
