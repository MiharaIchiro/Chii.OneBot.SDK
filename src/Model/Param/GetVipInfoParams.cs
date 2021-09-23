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
    /// 獲取會員信息
    /// </summary>
    internal class GetVipInfoParams : IParams
    {
        /// <summary>
        /// 要查詢的 QQ 號
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
    }
}
