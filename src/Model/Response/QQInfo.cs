using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Response
{
    public class QQInfo
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
        /// QQ 等級
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }

        /// <summary>
        /// 等級加速度
        /// </summary>
        [JsonPropertyName("level_speed")]
        public double LevelSpeed { get; set; }

        /// <summary>
        /// 會員等級
        /// </summary>
        [JsonPropertyName("vip_level")]
        public string VipLevel { get; set; }

        /// <summary>
        /// 會員成長速度
        /// </summary>
        [JsonPropertyName("vip_growth_speed")]
        public int VipGrowthSpeed { get; set; }

        /// <summary>
        /// 會員成長總值
        /// </summary>
        [JsonPropertyName("vip_growth_total")]
        public int VipGrowthTotal { get; set; }
    }


}
