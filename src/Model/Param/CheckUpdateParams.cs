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
    /// 檢查更新
    /// 此接口 status 會返回 async，檢查更新操作將會在線程池執行。
    /// </summary>
    internal class CheckUpdateParams : IParams
    {
        /// <summary>
        /// 是否自動進行，如果為 true，將不會彈窗提示，而僅僅輸出日誌，同時如果 auto_perform_update 為 true，則會自動更新（需要手動重啟 機器人）
        /// </summary>
        [JsonPropertyName("automatic")]
        public bool Automatic { get; set; }
    }
}
