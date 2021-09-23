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
    ///  <para>獲取群成員列表</para>
    ///  <para>響應內容為 JSON 數組，每個元素的內容和上面的 /get_group_member_info 接口相同，</para>
    ///  <para>但對於同一個群組的同一個成員，獲取列表時和獲取單獨的成員信息時，某些字段可能有所不同，</para>
    ///  <para>例如 area、title 等字段在獲取列表時無法獲得，具體應以單獨的成員信息為準。</para>
    /// </summary>
    internal class GetGroupMemberListParams : IParams
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }
    }
}
