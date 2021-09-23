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
    /// 設置群名片（群備註）
    /// </summary>
    internal class SetGroupCardParams : IParams
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 	要設置的 QQ 號
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// 群名片內容，不填或空字符串表示刪除群名片
        /// </summary>
        [JsonPropertyName("card")]
        public string Card { get; set; }
    }
}
