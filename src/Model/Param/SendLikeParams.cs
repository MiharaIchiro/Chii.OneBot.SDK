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
    /// 發送好友贊
    /// </summary>
    internal class SendLikeParams : IParams
    {
        /// <summary>
        /// 對方 QQ 號
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// 贊的次數，每個好友每天最多 10 次
        /// </summary>
        [JsonPropertyName("times")]
        public byte Times { get; set; }
    }
}
