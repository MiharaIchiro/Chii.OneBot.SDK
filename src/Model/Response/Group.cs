using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Response
{
    public class Group
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// 群名稱
        /// </summary>
        [JsonPropertyName("group_name")]
        public string GroupName { get; set; }

        /// <summary>
        /// 創建時間戳
        /// </summary>
        [JsonPropertyName("create_time")]
        public long CreatTimeStamp { get; set; }

        /// <summary>
        /// 創建時間
        /// </summary>
        [JsonIgnore()]
        public DateTime CreatTime
        {
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(CreatTimeStamp).DateTime;
            }
        }

        /// <summary>
        /// 群分類，具體這個 ID 對應的名稱暫時沒有
        /// </summary>
        [JsonPropertyName("category")]
        public int Category { get; set; }

        /// <summary>
        /// 成員數
        /// </summary>
        [JsonPropertyName("member_count")]
        public int MemberCount { get; set; }

        /// <summary>
        /// 最大成員數（群容量）
        /// </summary>
        [JsonPropertyName("max_member_count")]
        public int MaxMemberCount { get; set; }

        /// <summary>
        /// 群介紹
        /// </summary>
        [JsonPropertyName("introduction")]
        public string Introduction { get; set; }

        /// <summary>
        /// 群主和管理員列表
        /// </summary>
        [JsonPropertyName("admins")]
        public IReadOnlyList<Admin> Admins { get; set; }

        /// <summary>
        /// 群主和管理員數
        /// </summary>
        [JsonPropertyName("admin_count")]
        public int AdminCount { get; set; }

        /// <summary>
        /// 最大群主和管理員數
        /// </summary>
        [JsonPropertyName("max_admin_count")]
        public int MaxAdminCount { get; set; }

        /// <summary>
        /// 群主 QQ 號
        /// </summary>
        [JsonPropertyName("owner_id")]
        public long OwnerId { get; set; }

    }
}
