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
    /// 重啟 機器人，並以當前登錄號自動登錄（需勾選快速登錄）
    /// </summary>
    internal class SetRestartParams : IParams
    {
        /// <summary>
        /// 是否在重啟時清空 機器人 的日誌數據庫（log*.db）
        /// </summary>
        [JsonPropertyName("clean_log")]
        public bool CleanLog { get; set; }

        /// <summary>
        /// 是否在重啟時清空 機器人 的緩存數據庫（cache.db）
        /// </summary>
        [JsonPropertyName("clean_cache")]
        public bool CleanCache { get; set; }

        /// <summary>
        /// 是否在重啟時清空 機器人 的事件數據庫（eventv2.db）
        /// </summary>
        [JsonPropertyName("clean_event")]
        public bool CleanEvent { get; set; }
    }
}
