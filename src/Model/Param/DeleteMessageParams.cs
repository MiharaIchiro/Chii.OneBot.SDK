using Chii.OneBot.SDK.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Params
{
    /// <summary>
    /// 撤回消息
    /// </summary>
    internal class DeleteMessageParams : IParams
    {
        /// <summary>
        /// 消息 ID
        /// </summary>
        [JsonPropertyName("message_id")]
        public int MessageId { get; set; }
    }
}
