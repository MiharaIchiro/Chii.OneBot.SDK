using Chii.OneBot.SDK.Enumeration;
using Chii.OneBot.SDK.WebSocket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model
{
    public class Source
    {
        /// <summary>
        /// <para>原始事件數據對象</para>
        /// </summary>
        public string Context { get; private set; }

        /// <summary>
        /// <para>接收者</para>
        /// </summary>
        public string SelfId { get; private set; }

        /// <summary>
        /// <para>接收時間</para>
        /// </summary>
        public DateTime ReceivedDate { get; private set; }

        /// <summary>
        /// <para>連接器數據</para>
        /// </summary>
        public ConnectionData ConnectionData { get; private set; }

        /// <summary>
        /// <para>發送</para>
        /// </summary>
        public Func<string, Task> Send { get; private set; }

        /// <summary>
        /// <para>QQ 號</para>
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// <para>群號</para>
        /// </summary>
        public long GroupId { get; private set; }

        /// <summary>
        /// <para>討論組 ID</para>
        /// </summary>
        public long DiscussId { get; private set; }

        /// <summary>
        /// 消息識別分類
        /// </summary>
        public MessageFlags Flags { get; private set; }


        public Source(string selfId, DateTime receivedDate, MessageEventArgs MessageEventArgs)
        {
            this.SelfId = selfId;
            this.Context = MessageEventArgs.Message;
            this.ReceivedDate = receivedDate;
            this.ConnectionData = MessageEventArgs.ConnectionData;
            this.Send = MessageEventArgs.Connection.Send;
        }

        public void UpdateSource(long userId, long groupId, long discussId, MessageFlags flags)
        {
            this.UserId = userId;
            this.GroupId = groupId;
            this.DiscussId = discussId;
            this.Flags = flags;
        }
    }
}
