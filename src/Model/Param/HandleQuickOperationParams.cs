using Chii.OneBot.SDK.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Params
{
    /// <summary>
    /// 對事件執行快速操作
    /// </summary>
    internal class HandleQuickOperationParams : IParams
    {
        /// <summary>
        /// 事件上報的數據對象
        /// </summary>
        [JsonPropertyName("context")]
        public JsonElement Context { get; set; }

        /// <summary>
        /// 快速操作對象，例如 {"ban": true, "reply": "請不要說髒話"}
        /// </summary>
        [JsonPropertyName("operation")]
        public QuickOperation Operation { get; set; }
    }
}
