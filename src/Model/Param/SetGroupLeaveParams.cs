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
    /// 退出群組
    /// </summary>
    internal class SetGroupLeaveParams : IParams
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 是否解散，如果登錄號是群主，則僅在此項為 true 時能夠解散
        /// </summary>
        [JsonPropertyName("is_dismiss")]
        public bool IsDismiss { get; set; }

    }
}
