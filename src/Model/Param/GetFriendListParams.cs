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
    /// 獲取好友列表
    /// </summary>
    internal class GetFriendListParams : IParams
    {
        /// <summary>
        /// 是否獲取扁平化的好友數據，即所有好友放在一起、所有分組放在一起，而不是按分組層級
        /// </summary>
        [JsonPropertyName("flat")]
        public bool Flat { get; set; }
    }
}
