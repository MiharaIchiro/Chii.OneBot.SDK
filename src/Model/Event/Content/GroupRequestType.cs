using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model
{
    public enum GroupRequestType : byte
    {
        /// <summary>
        /// 添加
        /// </summary>
        Add = 0,

        /// <summary>
        /// 邀請
        /// </summary>
        Invite = 1,
    }
}
