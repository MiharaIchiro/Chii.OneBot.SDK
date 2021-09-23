using Chii.OneBot.SDK.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Resource
{
    /// <summary>
    /// CQHTTP 回調合集
    /// </summary>
    public class ResponseResource
    {
        /// <summary>
        /// 操作結果返回碼
        /// </summary>
        public int Retcode { get; set; }

        /// <summary>
        /// 操作失敗與否
        /// </summary>
        public bool IsFailed { get; set; }

        /// <summary>
        /// 操作無效與否
        /// </summary>
        public bool IsInVaild { get; set; } = true;

        /// <summary>
        /// 消息Id
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// 好友列表
        /// </summary>
        public IList<FriendGroup> FriendGroupList { get; set; }

        /// <summary>
        /// 登錄號信息
        /// </summary>
        public LoginInfo LoginInfo { get; set; }

        /// <summary>
        /// 陌生人信息,會員信息
        /// </summary>
        public QQInfo QQInfo { get; set; }

        /// <summary>
        /// 群列表
        /// </summary>
        public IList<Group> GroupList { get; set; }

        /// <summary>
        /// 群成員,群成員列表
        /// </summary>
        public IList<GroupMemberInfo> GroupMemberList { get; set; }

        /// <summary>
        /// QQ 相關接口憑證
        /// </summary>
        public Credentials Credentials { get; set; }

        /// <summary>
        /// 圖片語音文件信息
        /// </summary>
        public FileInfo File { get; set; }

        /// <summary>
        /// 插件運行狀態
        /// </summary>
        public OneBotStatus Status { get; set; }

        /// <summary>
        /// 機器人 及 HTTP API 插件的版本信息
        /// </summary>
        public OneBotVersion Version { get; set; }

        /// <summary>
        /// 是否可以發送圖片
        /// </summary>
        public bool CanSendImage { get; set; }

        /// <summary>
        /// 是否可以發送語音
        /// </summary>
        public bool CanSendRecord { get; set; }
    }

}
