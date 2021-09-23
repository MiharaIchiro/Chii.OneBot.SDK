using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Response
{
    public class Admin
    {
        /// <summary>
        /// QQ 號
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// QQ 昵稱
        /// </summary>
        [JsonPropertyName("nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// 角色，owner 表示群主、admin 表示管理員
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }
    }

}
