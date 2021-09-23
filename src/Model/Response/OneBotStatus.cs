using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Response
{
    public class OneBotStatus
    {
        /// <summary>
        /// HTTP API 插件已初始化
        /// </summary>
        [JsonPropertyName("app_initialized")]
        public bool AppInitialized { get; set; }

        /// <summary>
        /// HTTP API 插件已啟用
        /// </summary>
        [JsonPropertyName("app_enabled")]
        public bool App_enabled { get; set; }

        /// <summary>
        /// HTTP API 的各內部插件是否正常運行
        /// </summary>
        [JsonPropertyName("plugins_good")]
        public JsonElement PluginsGood { get; set; }

        /// <summary>
        /// HTTP API 插件正常運行（已初始化、已啟用、各內部插件正常運行）
        /// </summary>
        [JsonPropertyName("app_good")]
        public bool AppGood { get; set; }

        /// <summary>
        /// 	當前 QQ 在線
        /// </summary>
        [JsonPropertyName("online")]
        public bool Online { get; set; }

        /// <summary>
        /// 	HTTP API 插件狀態符合預期，意味著插件已初始化，內部插件都在正常運行，且 QQ 在線
        /// </summary>
        [JsonPropertyName("good")]
        public bool Good { get; set; }
    }

}
