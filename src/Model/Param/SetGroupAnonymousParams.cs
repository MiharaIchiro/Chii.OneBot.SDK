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
    /// 群組匿名
    /// </summary>
    internal class SetGroupAnonymousParams : IParams
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 是否允許匿名聊天
        /// </summary>
        [JsonPropertyName("enable")]
        public bool Enable { get; set; } = true;
    }
}
