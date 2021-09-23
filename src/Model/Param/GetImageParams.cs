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
    /// 獲取圖片
    /// </summary>
    internal class GetImageParams : IParams
    {
        /// <summary>
        /// 收到的圖片文件名（CQ 碼的 file 參數），如 <see href="6B4DE3DFD1BD271E3297859D41C530F5.jpg"/>
        /// </summary>
        [JsonPropertyName("file")]
        public string File { get; set; }
    }
}
