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
    /// 獲取群信息
    /// </summary>
    internal class GetGroupInfoParams : IParams
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 是否不使用緩存（使用緩存可能更新不及時，但響應更快）
        /// </summary>
        [JsonPropertyName("no_cache")]
        public bool NoCache { get; set; }
    }
}
