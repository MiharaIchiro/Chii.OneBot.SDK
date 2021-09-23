using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Response
{

    public class GroupMemberInfo
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// QQ 號
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// 昵稱
        /// </summary>
        [JsonPropertyName("nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// 群名片／備註
        /// </summary>
        [JsonPropertyName("card")]
        public string Card { get; set; }

        /// <summary>
        /// 性別，male 或 female 或 unknown
        /// </summary>
        [JsonPropertyName("sex")]
        public string Sex { get; set; }

        /// <summary>
        /// 年齡
        /// </summary>
        [JsonPropertyName("age")]
        public int Age { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        [JsonPropertyName("area")]
        public string Area { get; set; }

        /// <summary>
        /// 加群時間戳
        /// </summary>
        [JsonPropertyName("join_time")]
        public int JoinTime { get; set; }

        /// <summary>
        /// 最後發言時間戳
        /// </summary>
        [JsonPropertyName("last_sent_time")]
        public int LastSentTime { get; set; }

        /// <summary>
        /// 成員等級
        /// </summary>
        [JsonPropertyName("level")]
        public string Level { get; set; }

        /// <summary>
        /// 角色，owner 或 admin 或 member
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// 是否不良記錄成員
        /// </summary>
        [JsonPropertyName("unfriendly")]
        public bool Unfriendly { get; set; }

        /// <summary>
        /// 專屬頭銜
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// 專屬頭銜過期時間戳
        /// </summary>
        [JsonPropertyName("title_expire_time")]
        public int TitleExpireTime { get; set; }

        /// <summary>
        /// 是否允許修改群名片
        /// </summary>
        [JsonPropertyName("card_changeable")]
        public bool CardChangeable { get; set; }
    }

}
