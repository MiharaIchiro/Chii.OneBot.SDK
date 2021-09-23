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
    /// 獲取 QQ 相關接口憑證
    /// </summary>
    internal class GetCredentialsParams : IParams
    {
        /// <summary>
        /// 需要獲取 cookies 的域名
        /// </summary>
        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// CSRF Token
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
