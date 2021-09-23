using Chii.OneBot.SDK.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Params
{
    /// <summary>
    /// 群組踢人
    /// </summary>
    internal class SetGroupKickParams : IParams
    {
        /// <summary>
        /// 要踢的 QQ 號
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 拒絕此人的加群請求
        /// </summary>
        [JsonPropertyName("reject_add_request")]
        public bool RejectAddRequest { get; set; }
    }
}
