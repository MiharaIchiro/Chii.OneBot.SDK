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
    /// 設置群組專屬頭銜
    /// </summary>
    internal class SetGroupSpecialTitleParams : IParams
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 要設置的 QQ 號
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// 專屬頭銜，不填或空字符串表示刪除專屬頭銜
        /// </summary>
        [JsonPropertyName("special_title")]
        public string SpecialTitle { get; set; }

        /// <summary>
        /// 專屬頭銜有效期，單位秒，-1 表示永久，不過此項似乎沒有效果，可能是只有某些特殊的時間長度有效，有待測試
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }

    }
}
