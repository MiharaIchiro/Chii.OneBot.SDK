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
    internal class SetRestartPluginParams : IParams
    {
        /// <summary>
        /// 要延遲的毫秒數，如果默認情況下無法重啟，可以嘗試設置延遲為 2000 左右
        /// </summary>
        [JsonPropertyName("delay")]
        public int Delay { get; set; }
    }
}
