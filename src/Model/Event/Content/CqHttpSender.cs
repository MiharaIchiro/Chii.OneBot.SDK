using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model
{
    public sealed class CqHttpSender
    {
        /// <summary>
        /// QQ號
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// QQ 昵稱
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 群名片／備註
        /// </summary>
        public string Card { get; set; }

        /// <summary>
        /// 專屬頭銜
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 年齡
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 成員等級
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public InGroupSex Sex { get; set; } = InGroupSex.Unknown;

        /// <summary>
        /// 角色
        /// </summary>
        public GroupRole Role { get; set; } = GroupRole.Member;
    }
}
