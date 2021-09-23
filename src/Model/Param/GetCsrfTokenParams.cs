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
    /// 獲取 CSRF Token
    /// </summary>
    internal class GetCsrfTokenParams : IParams
    {
        /// <summary>
        /// CSRF Token
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
