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
    /// <para>獲取語音</para>
    /// <para>其實並不是真的獲取語音，而是轉換語音到指定的格式，然後返回語音文件名（data\record 目錄下）。</para>
    /// <para>注意，要使用此接口，需要安裝 機器人 的 ffmpeg 組件</see>。</para>
    /// </summary>
    internal class GetRecordParams : IParams
    {
        /// <summary>
        /// 收到的語音文件名（CQ 碼的 file 參數），如 <see href="0B38145AA44505000B38145AA4450500.silk"/>
        /// </summary>
        [JsonPropertyName("file")]
        public string File { get; set; }

        /// <summary>
        /// 要轉換到的格式，目前支持 mp3、amr、wma、m4a、spx、ogg、wav、flac
        /// </summary>
        [JsonPropertyName("out_format")]
        public string OutFormat { get; set; }

        /// <summary>
        /// 是否返迴文件的絕對路徑（Windows 環境下建議使用，Docker 中不建議）
        /// </summary>
        [JsonPropertyName("full_path")]
        public bool FullPath { get; set; } = false;
    }
}
