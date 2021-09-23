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
    /// 群組設置管理員
    /// </summary>
    internal class SetGroupAdminParams : IParams
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 要設置管理員的 QQ 號
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// true 為設置，false 為取消
        /// </summary>
        [JsonPropertyName("enable")]
        public bool Enable { get; set; }
    }
}
