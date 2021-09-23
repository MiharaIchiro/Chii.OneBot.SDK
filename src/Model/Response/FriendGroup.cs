using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Response
{
    public class FriendGroup
    {
        /// <summary>
        /// 好友分組 ID
        /// </summary>
        [JsonPropertyName("friend_group_id")]
        public int FriendGroupId { get; set; }

        /// <summary>
        /// 好友分組名稱
        /// </summary>
        [JsonPropertyName("friend_group_name")]
        public string FriendGroupName { get; set; }

        /// <summary>
        /// 分組中的好友
        /// </summary>
        [JsonPropertyName("friends")]
        public IList<Friend> Friends { get; set; }
    }
}
