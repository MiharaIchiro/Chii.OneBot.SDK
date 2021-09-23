using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Response
{
    public class FileInfo
    {
        /// <summary>
        /// 下載後的圖片/轉換後的語音文件路徑
        /// </summary>
        [JsonPropertyName("file")]
        public string FilePath { get; set; }
    }

}
