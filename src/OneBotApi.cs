using System;
using System.Net;
using System.Linq;
using System.Data;
using System.Text.Json;
using System.Threading;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using Chii.OneBot.SDK.Model;
using Chii.OneBot.SDK.Params;
using Chii.OneBot.SDK.Resource;
using Chii.OneBot.SDK.WebSocket;
using Chii.OneBot.SDK.Model.Params;
using Chii.OneBot.SDK.Model.Response;
using Chii.OneBot.SDK.Enumeration;

namespace Chii.OneBot.SDK
{
    public static class OneBotApi
    {
        /// <summary>
        /// 回調超時限制
        /// </summary>
        public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// 回覆對方消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message">要發送的內容</param>
        /// <param name="atSender">@發送者</param>
        public static async ValueTask Reply(this Source source, string message, bool atSender = true)
        {
            await SendMessage(source, new CqHttpRequest()
            {
                Task = RequestType.HandleQuickOperation,
                Params = new HandleQuickOperationParams()
                {
                    Context = JsonDocument.Parse(source.Context).RootElement,
                    Operation = new QuickOperation()
                    {
                        Reply = message,
                        AtSender = atSender
                    }
                }
            });
        }

        /// <summary>
        /// 禁言對方(預設30分鐘)
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask Ban(this Source source)
        {
            await SendMessage(source, new CqHttpRequest()
            {
                Task = RequestType.HandleQuickOperation,
                Params = new HandleQuickOperationParams()
                {
                    Context = JsonDocument.Parse(source.Context).RootElement,
                    Operation = new QuickOperation()
                    {
                        Ban = true
                    }
                }
            });
        }

        /// <summary>
        /// 踢對方
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask Kick(this Source source)
        {
            await SendMessage(source, new CqHttpRequest()
            {
                Task = RequestType.HandleQuickOperation,
                Params = new HandleQuickOperationParams()
                {
                    Context = JsonDocument.Parse(source.Context).RootElement,
                    Operation = new QuickOperation()
                    {
                        Kick = true
                    }
                }
            });
        }

        /// <summary>
        /// 同意群組/好友請求
        /// </summary>
        /// <param name="source"></param>
        /// <param name="remark">好友備註</param>
        public static async ValueTask Approve(this Source source, string remark = "")
        {
            await SendMessage(source, new CqHttpRequest()
            {
                Task = RequestType.HandleQuickOperation,
                Params = new HandleQuickOperationParams()
                {
                    Context = JsonDocument.Parse(source.Context).RootElement,
                    Operation = new QuickOperation()
                    {
                        Approve = true,
                        Remark = remark
                    }
                }
            });
        }

        /// <summary>
        /// 拒絕群組/好友請求
        /// </summary>
        /// <param name="source"></param>
        /// <param name="reson">拒絕理由</param>
        public static async ValueTask Disapprove(this Source source, string reson = "")
        {
            await SendMessage(source, new CqHttpRequest()
            {
                Task = RequestType.HandleQuickOperation,
                Params = new HandleQuickOperationParams()
                {
                    Context = JsonDocument.Parse(source.Context).RootElement,
                    Operation = new QuickOperation()
                    {
                        Approve = false,
                        Reason = reson
                    }
                }
            });
        }

        /// <summary>
        /// 撤回對方消息
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask Delete(this Source source)
        {
            await SendMessage(source, new CqHttpRequest()
            {
                Task = RequestType.HandleQuickOperation,
                Params = new HandleQuickOperationParams()
                {
                    Context = JsonDocument.Parse(source.Context).RootElement,
                    Operation = new QuickOperation()
                    {
                        Delete = true
                    }
                }
            });
        }

        /// <summary>
        /// 發送消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message">要發送的內容</param>
        /// <param name="autoEscape">消息內容是否作為純文本發送（即不解析 CQ 碼），</param>
        public static async ValueTask<int> SendMessage(this Source source, string message, bool autoEscape = false)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SendMsg,
                Params = new MessageParams()
                {
                    MessageType =
                    source.Flags.HasFlag(MessageFlags.Group) ?
                        source.Flags.HasFlag(MessageFlags.Discuss) ?
                            MessageFlags.Discuss :
                        MessageFlags.Group :
                    MessageFlags.Private,
                    Message = message,
                    UserId = source.UserId,
                    GroupId = source.GroupId,
                    DiscussId = source.DiscussId,
                    MessageAutoEscape = autoEscape
                }
            });
            return result?.MessageId ?? -1;
        }

        /// <summary>
        /// 發送私聊消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="userId">對方 QQ 號</param>
        /// <param name="message">要發送的內容</param>
        /// <param name="auto_escape">消息內容是否作為純文本發送（即不解析 CQ 碼），</param>
        public static async ValueTask<int> SendPrivateMessage(this Source source, string message, long userId = 0, bool autoEscape = false)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SendMsg,
                Params = new MessageParams()
                {
                    MessageType = MessageFlags.Group,
                    Message = message,
                    UserId = userId,
                    MessageAutoEscape = autoEscape
                }
            });
            return result?.MessageId ?? -1;
        }

        /// <summary>
        /// 發送群組匿名消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="message">要發送的內容</param>
        /// <param name="auto_escape">消息內容是否作為純文本發送（即不解析 CQ 碼），</param>
        public static async ValueTask<ResponseResource> SendGroupAnonymousMessage(this Source source, string message, long groupId = 0, bool autoEscape = false)
        {
            return await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SendMsg,
                Params = new MessageParams()
                {
                    MessageType = MessageFlags.Group,
                    Message = $"[CQ:anonymous,ignore=true]{message}",
                    GroupId = groupId,
                    MessageAutoEscape = autoEscape,
                }
            });
        }

        /// <summary>
        /// 發送群組消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="message">要發送的內容</param>
        /// <param name="auto_escape">消息內容是否作為純文本發送（即不解析 CQ 碼），</param>
        public static async ValueTask<ResponseResource> SendGroupMessage(this Source source, string message, long groupId = 0, bool autoEscape = false)
        {
            return await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SendMsg,
                Params = new MessageParams()
                {
                    MessageType = MessageFlags.Group,
                    Message = message,
                    GroupId = groupId,
                    MessageAutoEscape = autoEscape,
                }
            });
        }

        /// <summary>
        /// 發送討論組消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="discussId">討論組 ID（正常情況下看不到，需要從討論組消息上報的數據中獲得）</param>
        /// <param name="message">要發送的內容</param>
        /// <param name="auto_escape">消息內容是否作為純文本發送（即不解析 CQ 碼），</param>
        public static async ValueTask<ResponseResource> SendDiscussMessage(this Source source, string message, long discussId = 0, bool autoEscape = false)
        {
            return await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SendMsg,
                Params = new MessageParams()
                {
                    MessageType = MessageFlags.Discuss,
                    Message = message,
                    DiscussId = discussId,
                    MessageAutoEscape = autoEscape,
                }
            });
        }

        /// <summary>
        /// 撤回消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="messageId">消息 ID</param>
        public static async ValueTask DeleteMessage(this Source source, int messageId)
        {
            await SendMessage(source, new CqHttpRequest()
            {
                Task = RequestType.DeleteMsg,
                Params = new DeleteMessageParams()
                {
                    MessageId = messageId
                }
            });
        }

        /// <summary>
        /// 發送好友贊
        /// </summary>
        /// <param name="source"></param>
        /// <param name="userId">對方 QQ 號</param>
        /// <param name="times">贊的次數，每個好友每天最多 10 次</param>
        public static async ValueTask SendLike(this Source source, long? userId = null, byte times = 1)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SendLike,
                Params = new SendLikeParams()
                {
                    UserId = userId ?? source.UserId,
                    Times = times
                }
            });
        }

        /// <summary>
        /// 群組踢人
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="userId">要踢的 QQ 號</param>
        /// <param name="rejectAddRequest">拒絕此人的加群請求</param>
        public static async ValueTask SetGroupKick(this Source source, long? groupId = null, long? userId = null, bool rejectAddRequest = false)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetGroupKick,
                Params = new SetGroupKickParams()
                {
                    GroupId = groupId ?? source.GroupId,
                    UserId = userId ?? source.UserId,
                    RejectAddRequest = rejectAddRequest
                }
            });
        }

        /// <summary>
        /// 群組單人禁言
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="duration">禁言時長，單位秒，0 表示取消禁言</param>
        public static async ValueTask SetGroupBan(this Source source, long? groupId = null, int duration = 1800)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetGroupBan,
                Params = new SetGroupBanParams()
                {
                    GroupId = groupId ?? source.GroupId,
                    Duration = duration,
                }
            });
        }

        /// <summary>
        /// 群組匿名用戶禁言
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="flag">要禁言的匿名用戶的 flag（需從群消息上報的數據中獲得）</param>
        /// <param name="duration">禁言時長，單位秒，0 表示取消禁言</param>
        public static async ValueTask SetGroupAnonymousBan(this Source source, string flag, long? groupId = null, int duration = 1800)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetGroupAnonymousBan,
                Params = new SetGroupAnonymousBanParams()
                {
                    GroupId = groupId ?? source.GroupId,
                    Flag = flag,
                    Duration = duration,
                }
            });
        }

        /// <summary>
        /// 群組全員禁言
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="enable">是否禁言</param>
        /// <returns></returns>
        public static async ValueTask SetGroupWholeBan(this Source source, long? groupId = null, bool enable = true)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetGroupWholeBan,
                Params = new SetGroupWholeBanParams()
                {
                    GroupId = groupId ?? source.GroupId,
                    Enable = enable
                }
            });
        }

        /// <summary>
        /// 群組設置管理員
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="userId">要設置管理員的 QQ 號</param>
        /// <param name="enable">true 為設置，false 為取消</param>
        /// <returns></returns>
        public static async ValueTask SetGroupAdmin(this Source source, long? groupId, long? userId = null, bool enable = true)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetGroupAdmin,
                Params = new SetGroupAdminParams()
                {
                    GroupId = groupId ?? source.GroupId,
                    UserId = userId ?? source.UserId,
                    Enable = enable
                }
            });
        }

        /// <summary>
        /// 群組匿名
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="enable">是否允許匿名聊天</param>
        /// <returns></returns>
        public static async ValueTask SetGroupAnonymous(this Source source, long? groupId = null, bool enable = true)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetGroupAnonymous,
                Params = new SetGroupAnonymousParams()
                {
                    GroupId = groupId ?? source.GroupId,
                    Enable = enable
                }
            });
        }

        /// <summary>
        /// 設置群名片（群備註）
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="userId">要設置的 QQ 號</param>
        /// <param name="card">群名片內容，不填或空字符串表示刪除群名片</param>
        /// <returns></returns>
        public static async ValueTask SetGroupAnonymous(this Source source, string card, long? groupId = null, long? userId = null)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetGroupCard,
                Params = new SetGroupCardParams()
                {
                    GroupId = groupId ?? source.GroupId,
                    UserId = userId ?? source.UserId,
                    Card = card
                }
            });
        }

        /// <summary>
        /// 退出群組
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="isDismiss">是否解散，如果登錄號是群主，則僅在此項為 true 時能夠解散</param>
        /// <returns></returns>
        public static async ValueTask SetGroupLeave(this Source source, long? groupId = null, bool isDismiss = false)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetGroupLeave,
                Params = new SetGroupLeaveParams()
                {
                    GroupId = groupId ?? source.GroupId,
                    IsDismiss = isDismiss
                }
            });
        }

        /// <summary>
        /// 設置群組專屬頭銜
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="userId">要設置的 QQ 號</param>
        /// <param name="specialTitle">專屬頭銜，不填或空字符串表示刪除專屬頭銜</param>
        /// <param name="duration">專屬頭銜有效期，單位秒，-1 表示永久，不過此項似乎沒有效果，可能是只有某些特殊的時間長度有效，有待測試</param>
        /// <returns></returns>
        public static async ValueTask SetGroupLeave(this Source source, long groupId, long userId, string specialTitle, int duration = -1)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetGroupSpecialTitle,
                Params = new SetGroupSpecialTitleParams()
                {
                    GroupId = groupId,
                    UserId = userId,
                    SpecialTitle = specialTitle,
                    Duration = duration
                }
            });
        }

        /// <summary>
        /// 退出討論組
        /// </summary>
        /// <param name="source"></param>
        /// <param name="discussId">討論組 ID（正常情況下看不到，需要從討論組消息上報的數據中獲得）</param>
        /// <returns></returns>
        public static async ValueTask SetGroupLeave(this Source source, long? discussId = null)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetDiscussLeave,
                Params = new SetDiscussLeaveParams()
                {
                    DiscussId = discussId ?? source.DiscussId
                }
            });
        }

        /// <summary>
        /// 處理加好友請求
        /// </summary>
        /// <param name="source"></param>
        /// <param name="flag">加好友請求的 flag（需從上報的數據中獲得）</param>
        /// <param name="approve">是否同意請求</param>
        /// <param name="remark">添加後的好友備註（僅在同意時有效）</param>
        /// <returns></returns>
        public static async ValueTask SetGroupLeave(this Source source, string flag, bool approve = true, string remark = "")
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetFriendAddRequest,
                Params = new SetFriendAddRequestParams()
                {
                    Flag = flag,
                    Approve = approve,
                    Remark = remark
                }
            });
        }

        /// <summary>
        /// 處理加群請求／邀請
        /// </summary>
        /// <param name="source"></param>
        /// <param name="flag">加群請求的 flag（需從上報的數據中獲得）</param>
        /// <param name="type">請求類型</param>
        /// <param name="approve">是否同意請求</param>
        /// <param name="reason">拒絕理由（僅在拒絕時有效）</param>
        /// <returns></returns>
        public static async ValueTask SetGroupLeave(this Source source, string flag, GroupRequestType type, bool approve = true, string reason = "")
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetFriendAddRequest,
                Params = new SetGroupAddRequestParams()
                {
                    Flag = flag,
                    Type = type == GroupRequestType.Add ? "add" : "invite",
                    Approve = approve,
                    Reason = reason
                }
            }); ;
        }

        /// <summary>
        /// 獲取登錄號信息
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask<LoginInfo> GetLoginInfo(this Source source)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetLoginInfo
            });
            return result?.LoginInfo;
        }

        /// <summary>
        /// 獲取陌生人信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="userId">QQ 號</param>
        /// <param name="noCache">是否不使用緩存（使用緩存可能更新不及時，但響應更快）</param>
        public static async ValueTask<QQInfo> GetStrangerInfo(this Source source, long? userId = null, bool noCache = false)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetStrangerInfo,
                Params = new GetStrangerInfoParams()
                {
                    UserId = userId ?? source.UserId,
                    NoCache = noCache
                }
            });
            return result?.QQInfo;
        }

        /// <summary>
        /// 獲取群列表
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask<IList<Group>> GetGroupList(this Source source)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetGroupList
            });
            return result?.GroupList;
        }

        /// <summary>
        /// 獲取群成員信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        /// <param name="userId">QQ號</param>
        /// <param name="noCache">是否不使用緩存（使用緩存可能更新不及時，但響應更快）</param>
        public static async ValueTask<GroupMemberInfo> GetGroupMemberInfo(this Source source, long? groupId = null, long? userId = null, bool noCache = false)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetGroupMemberInfo,
                Params = new GetGroupMemberInfoParams()
                {
                    GroupId = groupId ?? source.GroupId,
                    UserId = userId ?? source.UserId,
                    NoCache = noCache
                }
            });
            return result?.GroupMemberList.AsEnumerable()?.FirstOrDefault();
        }

        /// <summary>
        /// 獲取群成員列表
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">群號</param>
        public static async ValueTask<IList<GroupMemberInfo>> GetGroupMemberList(this Source source, long? groupId = null)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetGroupMemberList,
                Params = new GetGroupMemberListParams()
                {
                    GroupId = groupId ?? source.GroupId
                }
            });
            return result?.GroupMemberList;
        }

        /// <summary>
        /// 獲取 Cookies
        /// </summary>
        /// <param name="source"></param>
        /// <param name="domain">需要獲取 cookies 的域名</param>
        public static async ValueTask<string> GetCookies(this Source source, string domain = "qq.com")
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetCookies,
                Params = new GetCookiesParams()
                {
                    Domain = domain
                }
            });
            return result?.Credentials.Cookies;
        }

        /// <summary>
        /// 獲取 CSRF Token
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask<int> GetCsrfToken(this Source source)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetCsrfToken
            });
            return result?.Credentials.CsrfToken ?? -1;
        }

        /// <summary>
        /// 獲取 QQ 相關接口憑證
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask<Credentials> GetCredentials(this Source source)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetCredentials
            });
            return result?.Credentials;
        }

        /// <summary>
        /// 獲取語音
        /// <para>其實並不是真的獲取語音，而是轉換語音到指定的格式，然後返回語音文件名（data\record 目錄下）。</para>
        /// <para>注意，要使用此接口，需要安裝 ffmpeg 語音組件</see>。</para>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="file">收到的語音文件名</param>
        /// <param name="outFormat">要轉換到的格式，目前支持 mp3、amr、wma、m4a、spx、ogg、wav、flac</param>
        /// <param name="fullPath">是否返迴文件的絕對路徑（Windows 環境下建議使用，Docker 中不建議）</param>
        public static async ValueTask<FileInfo> GetRecord(this Source source, string file, string outFormat, bool fullPath = false)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetRecord,
                Params = new GetRecordParams()
                {
                    File = file,
                    OutFormat = outFormat,
                    FullPath = fullPath
                }
            });
            return result?.File;
        }

        /// <summary>
        /// 獲取圖片
        /// </summary>
        /// <param name="source"></param>
        /// <param name="file">收到的語音文件名</param>
        public static async ValueTask<FileInfo> GetRecord(this Source source, string file)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetImage,
                Params = new GetImageParams()
                {
                    File = file,
                }
            });
            return result?.File;
        }

        /// <summary>
        /// 檢查是否可以發送圖片
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask<bool> CanSendImage(this Source source)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.CanSendImage,
            });
            return result?.CanSendImage ?? false;
        }

        /// <summary>
        /// 檢查是否可以發送語音
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask<bool> CanSendRecord(this Source source)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.CanSendRecord,
            });
            return result?.CanSendRecord ?? false;
        }

        /// <summary>
        /// 獲取插件運行狀態
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask<OneBotStatus> GetStatus(this Source source)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetStatus,
            });
            return result?.Status;
}

        /// <summary>
        /// 獲取機器人及 OneBot API 插件的版本信息
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask<OneBotVersion> GetVersionInfo(this Source source)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetVersionInfo,
            });
            return result?.Version;
        }

        /// <summary>
        /// 重啟機器人，並以當前登錄號自動登錄（需勾選快速登錄）
        /// </summary>
        /// <param name="source"></param>
        /// <param name="cleanLog">是否在重啟時清空機器人的日誌數據庫</param>
        /// <param name="cleanCache">是否在重啟時清空機器人的緩存數據庫</param>
        /// <param name="cleanEvent">是否在重啟時清空機器人的事件數據庫</param>
        public static async ValueTask SetRestart(this Source source, bool cleanLog = false, bool cleanCache = false, bool cleanEvent = false)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetRestart,
                Params = new SetRestartParams()
                {
                    CleanLog = cleanLog,
                    CleanCache = cleanCache,
                    CleanEvent = cleanEvent
                }
            });
        }

        /// <summary>
        /// 重啟 HTTP API 插件
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask SetRestartPlugin(this Source source, int delay = 1)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.SetRestartPlugin,
                Params = new SetRestartPluginParams()
                {
                    Delay = delay
                }
            });
        }

        /// <summary>
        /// 清理數據目錄
        /// 用於清理積攢了太多舊文件的數據目錄，如 image。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="data_dir">收到清理的目錄名，支持 image、record、show、bface</param>
        public static async ValueTask CleanDataDir(this Source source, string data_dir)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.CleanDataDir,
                Params = new CleanDataDirParams()
                {
                    DataDir = data_dir
                }
            });
        }

        /// <summary>
        /// 用於清空插件的日誌文件。
        /// </summary>
        /// <param name="source"></param>
        public static async ValueTask CleanPluginLog(this Source source)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.CleanPluginLog
            });
        }

        /// <summary>
        /// 獲取好友列表
        /// </summary>
        /// <param name="source"></param>
        /// <param name="flat">是否獲取扁平化的好友數據，即所有好友放在一起、所有分組放在一起，而不是按分組層級</param>
        public static async ValueTask<IList<FriendGroup>> GetFriendList(this Source source, bool flat = false)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetFriendList,
                Params = new GetFriendListParams()
                {
                    Flat = flat
                }
            });
            return result?.FriendGroupList;
        }

        /// <summary>
        /// 獲取群信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="groupId">要查詢的群號</param>
        public static async ValueTask<Group> GetGroupInfo(this Source source, long? groupId = null)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetGroupInfo,
                Params = new GetGroupInfoParams()
                {
                    GroupId = groupId ?? source.GroupId
                }
            }); ;
            return result?.GroupList.AsEnumerable()?.FirstOrDefault();
        }

        /// <summary>
        /// 獲取會員信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="userId">要查詢的 QQ 號</param>
        public static async ValueTask<QQInfo> GetVipInfo(this Source source, long? userId = null)
        {
            var result = await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.GetVipInfo,
                Params = new GetVipInfoParams()
                {
                    UserId = userId ?? source.UserId
                }
            });
            return result?.QQInfo;
        }

        /// <summary>
        /// 檢查更新
        /// </summary>
        /// <param name="source"></param>
        /// <param name="automatic">是否自動進行，如果為 true，將不會彈窗提示，而僅僅輸出日誌，同時如果 auto_perform_update 為 true，則會自動更新並重啟機器人</param>
        public static async ValueTask CheckUpdate(this Source source, bool automatic = false)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.CheckUpdate,
                Params = new CheckUpdateParams()
                {
                    Automatic = automatic
                }
            });
        }

        /// <summary>
        /// 對事件執行快速操作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static async ValueTask HandleQuickOperation(this Source source, string context, QuickOperation operation)
        {
            await SendRequestMessage(source, new CqHttpRequest()
            {
                Task = RequestType.HandleQuickOperation,
                Params = new HandleQuickOperationParams()
                {
                    Context = JsonDocument.Parse(context).RootElement,
                    Operation = operation
                }
            });
        }

        /// <summary>
        /// 發送原始請求消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="timeout"></param>
        private static async Task SendMessage(this Source source, CqHttpRequest message)
        {
            var api = source.ConnectionData.RoleAndConnections.TryGetValue("API", out Connection conn);
            if (api)
            {
                await conn.Send(JsonSerializer.Serialize(message, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.Default }));
            }
        }

        /// <summary>
        /// 發送請求消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="timeout"></param>
        private static async Task<ResponseResource> SendRequestMessage(this Source source, CqHttpRequest message)
        {
            var api = source.ConnectionData.RoleAndConnections.TryGetValue("API", out Connection conn);
            if (api)
            {
                message.Echo = $"~{message.Task}@{Guid.NewGuid()}";
                await conn.Send(JsonSerializer.Serialize(message, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.Default }));
                return await ActionResource.OneBotSubject
                    .Where(r => r.Item1 == message.Echo).Select(r => r.Item2).Take(1)
                    .Timeout(Timeout).Catch(Observable.Return<ResponseResource>(null)).ToTask();
            }
            return null;
        }
        public static void SetResult(string echo, ResponseResource result)
        {
            ActionResource.OneBotSubject.OnNext(Tuple.Create(echo, result));
        }

    }
}
