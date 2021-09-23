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
    /// 清理數據目錄
    /// </summary>
    internal class CleanDataDirParams : IParams
    {
        /// <summary>
        /// 收到清理的目錄名，支持 image、record、show、bface
        /// </summary>
        [JsonPropertyName("data_dir")]
        public string DataDir { get; set; }
    }
}
