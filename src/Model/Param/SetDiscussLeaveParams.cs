using Chii.OneBot.SDK.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Params
{
    /// <summary>
    /// 發送討論組消息
    /// </summary>
    internal class SetDiscussLeaveParams : IParams
    {
        /// <summary>
        /// 討論組 ID（正常情況下看不到，需要從討論組消息上報的數據中獲得）
        /// </summary>
        [JsonPropertyName("discuss_id")]
        public long DiscussId { get; set; }

        /// <summary>
        /// 討論組 ID（正常情況下看不到，需要從討論組消息上報的數據中獲得）
        /// </summary>
        [JsonPropertyName("message")]
        public JsonElement Message { get; set; }


    }
}
