using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model
{
    public enum GroupRole : byte
    {
        /// <summary>
        /// 成員
        /// </summary>
        Member = 0,

        /// <summary>
        /// 管理員
        /// </summary>
        Admin = 1,

        /// <summary>
        /// 群主
        /// </summary>
        Owner = 255,
    }
}
