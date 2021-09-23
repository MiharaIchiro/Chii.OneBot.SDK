using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Chii.OneBot.SDK.Model;
using Chii.OneBot.SDK.Content;
using Chii.OneBot.SDK.Resource;
using Chii.OneBot.SDK.Enumeration;
using Chii.OneBot.SDK.Model.Event;
using Chii.OneBot.SDK.Model.Response;
using Chii.OneBot.SDK.WebSocket;

namespace Chii.OneBot.SDK
{
    public class OneBotParse
    {
        public delegate Task AsyncEventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;
        /// <summary>
        /// 連接事件
        /// </summary>
        public event AsyncEventHandler<ConnectEventArgs> OnConnectedAsync;
        /// <summary>
        /// 私聊事件
        /// </summary>
        public event AsyncEventHandler<CqHttpMessageEventArgs> OnPrivateMessageAsync;
        /// <summary>
        /// 群聊事件
        /// </summary>
        public event AsyncEventHandler<CqHttpMessageEventArgs> OnGroupMessageAsync;
        /// <summary>
        /// 討論組事件
        /// </summary>
        public event AsyncEventHandler<CqHttpMessageEventArgs> OnDiscussMessageAsync;
        /// <summary>
        /// 消息接收事件
        /// </summary>
        public event AsyncEventHandler<CqHttpMessageEventArgs> OnMessageAsync;
        /// <summary>
        /// 群通知事件
        /// </summary>
        public event AsyncEventHandler<CqGroupNoticEventArgs> OnGroupNoticAsync;
        /// <summary>
        /// 好友添加事件
        /// </summary>
        public event AsyncEventHandler<CqFriendAddEventArgs> OnFriendAddAsync;
        /// <summary>
        /// 好友請求事件
        /// </summary>
        public event AsyncEventHandler<CqFriendRequestEventArgs> OnFriendRequestAsync;
        /// <summary>
        /// 群組請求事件
        /// </summary>
        public event AsyncEventHandler<CqGroupRequestEventArgs> OnGroupRequestAsync;
        /// <summary>
        /// 狀態變更事件
        /// </summary>
        public event AsyncEventHandler<StatusEventArgs> OnStatusAsync;
        /// <summary>
        /// 回調消息事件
        /// </summary>
        public event AsyncEventHandler<ResponseEventArgs> OnResponseAsync;
        /// <summary>
        /// 消息處理失敗事件
        /// </summary>
        public event AsyncEventHandler<ResponseEventArgs> OnErrorResponseAsync;
        /// <summary>
        /// 解析失敗事件
        /// </summary>
        public event AsyncEventHandler<ResponseEventArgs> OnErrorParseAsync;
        /// <summary>
        /// 回調結果接口
        /// </summary>
        private readonly Action<string, ResponseResource> SetResult;
        /// <summary>
        /// CQHTTP消息解析
        /// </summary>
        /// <param name="setResult">回調存放點</param>
        public OneBotParse(Action<string, ResponseResource> setResult)
        {
            if (this.SetResult == null)
            {
                this.SetResult = setResult;
            }
        }
        /// <summary>
        /// 解析消息,事件分發
        /// </summary>
        /// <param name="selfId">消息來源</param>
        /// <param name="pack">消息封裝</param>
        /// <returns></returns>
        public async ValueTask Parse(object selfId, MessageEventArgs pack)
        {
            try
            {
                using (var docs = JsonDocument.Parse(pack.Message))
                {
                    await Task.Run(async () =>
                    {
                        DateTime receivedDate = DateTime.Now;
                        if (docs.RootElement.TryGetProperty("time", out JsonElement je_time))
                        {
                            if (je_time.ValueKind == JsonValueKind.Number)
                            {
                                if (je_time.TryGetInt64(out long timestamp))
                                {
                                    receivedDate = DateTimeOffset.FromUnixTimeSeconds(timestamp).AddHours(8).DateTime;
                                }
                            }
                        }
                        Source source = new Source(selfId.ToString(), receivedDate, pack);
                        if (docs.RootElement.TryGetProperty("post_type", out JsonElement je_ptype) == false)
                        {
                            await ParseResponse(source, docs.RootElement);
                            return;
                        }
                        if (je_ptype.ValueKind != JsonValueKind.String) { return; }
                        switch (je_ptype.GetString())
                        {
                            case "meta_event":
                                MetaEvent(docs.RootElement, source);
                                break;
                            case "message":
                                MessageEvent(docs.RootElement, source);
                                break;
                            case "notice":
                                NoticEvent(docs.RootElement, source);
                                break;
                            case "request":
                                RequestEvent(docs.RootElement, source);
                                break;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                await (OnErrorParseAsync?.Invoke(selfId, new ResponseEventArgs(null, new OneBotContent() { RetCode = -1, Status = ex.ToString() })) ?? Task.CompletedTask);
            }
        }
        /// <summary>
        /// 解析回調狀態
        /// </summary>
        /// <param name="source"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private async ValueTask<ResponseResource> ParseResponse(Source source, JsonElement element)
        {
            try
            {
                var response = JsonSerializer.Deserialize<OneBotContent>(element.GetRawText());
                ResponseResource data = new ResponseResource() { Retcode = response.RetCode };
                switch (response.Status)
                {
                    case "ok":
                    case "async":
                        await (OnResponseAsync?.Invoke(source.SelfId, new ResponseEventArgs(source, response)) ?? Task.CompletedTask);
                        if (response.Data.ValueKind != JsonValueKind.Null)
                            await ResponseParse(source, response.Echo, response.Data, data);
                        break;
                    case "failed":
                        await (OnErrorResponseAsync?.Invoke(source.SelfId, new ResponseEventArgs(source, response)) ?? Task.CompletedTask);
                        data.IsFailed = true;
                        this.SetResult(response.Echo, data);
                        break;
                }
                return data;

            }
            catch (Exception ex)
            {
                await (OnErrorParseAsync?.Invoke(source.SelfId, new ResponseEventArgs(source, new OneBotContent() { RetCode = -1, Status = ex.ToString() })) ?? Task.CompletedTask);
            }
            return null;
        }
        /// <summary>
        /// 解析回調消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="echo"></param>
        /// <param name="jData"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async ValueTask<ResponseResource> ResponseParse(Source source, string echo, JsonElement jData, ResponseResource data)
        {
            return await Task.Run(async () =>
            {
                if (echo.StartsWith("~") == false) { return data; }
                if (echo.Contains("@") == false) { return data; }
                data.IsInVaild = false;
                string[] ctx = echo.TrimStart('~').Split('@');
                RequestType task = (RequestType)System.Enum.Parse(typeof(RequestType), ctx[0]);
                try
                {
                    switch (task)
                    {
                        case RequestType.SendMsg:
                            if (jData.TryGetProperty("message_id", out JsonElement je_mid) == false) { break; }
                            if (je_mid.ValueKind != JsonValueKind.Number) { break; }
                            data.MessageId = je_mid.GetInt32();
                            break;
                        case RequestType.GetLoginInfo:
                            data.LoginInfo = JsonSerializer.Deserialize<LoginInfo>(jData.GetRawText());
                            break;
                        case RequestType.GetStrangerInfo:
                            data.QQInfo = JsonSerializer.Deserialize<QQInfo>(jData.GetRawText());
                            break;
                        case RequestType.GetGroupInfo:
                            data.GroupList = new List<Group>();
                            data.GroupList.Add(JsonSerializer.Deserialize<Group>(jData.GetRawText()));
                            break;
                        case RequestType.GetGroupList:
                            data.GroupList = JsonSerializer.Deserialize<IList<Group>>(jData.GetRawText());
                            break;
                        case RequestType.GetGroupMemberInfo:
                            data.GroupMemberList = new List<GroupMemberInfo>();
                            data.GroupMemberList.Add(JsonSerializer.Deserialize<GroupMemberInfo>(jData.GetRawText()));
                            break;
                        case RequestType.GetGroupMemberList:
                            data.GroupMemberList = JsonSerializer.Deserialize<IList<GroupMemberInfo>>(jData.GetRawText());
                            break;
                        case RequestType.GetCookies:
                        case RequestType.GetCredentials:
                            data.Credentials = JsonSerializer.Deserialize<Credentials>(jData.GetRawText());
                            break;
                        case RequestType.GetCsrfToken:
                            if (jData.TryGetProperty("token", out JsonElement je_token))
                            {
                                if (je_token.TryGetInt32(out int token))
                                {
                                    data.Credentials = new Credentials() { CsrfToken = token };
                                }
                            }
                            break;
                        case RequestType.GetRecord:
                        case RequestType.GetImage:
                            data.File = JsonSerializer.Deserialize<FileInfo>(jData.GetRawText());
                            break;
                        case RequestType.CanSendImage:
                            if (jData.TryGetProperty("yes", out JsonElement je_yes_image))
                            {
                                if (je_yes_image.ValueKind == JsonValueKind.True || je_yes_image.ValueKind == JsonValueKind.False)
                                {
                                    data.CanSendImage = je_yes_image.GetBoolean();
                                }
                            }
                            break;
                        case RequestType.CanSendRecord:
                            if (jData.TryGetProperty("yes", out JsonElement je_yes_record))
                            {
                                if (je_yes_record.ValueKind == JsonValueKind.True || je_yes_record.ValueKind == JsonValueKind.False)
                                {
                                    data.CanSendRecord = je_yes_record.GetBoolean();
                                }
                            }
                            break;
                        case RequestType.GetStatus:
                            data.Status = JsonSerializer.Deserialize<OneBotStatus>(jData.GetRawText());
                            break;
                        case RequestType.GetVersionInfo:
                            data.Version = JsonSerializer.Deserialize<OneBotVersion>(jData.GetRawText());
                            break;
                        case RequestType.GetFriendList:
                            data.FriendGroupList = JsonSerializer.Deserialize<IList<FriendGroup>>(jData.GetRawText());
                            break;
                        case RequestType.GetVipInfo:
                            data.QQInfo = JsonSerializer.Deserialize<QQInfo>(jData.GetRawText());
                            break;
                        case RequestType.DeleteMsg:
                        case RequestType.SendLike:
                        case RequestType.SetGroupKick:
                        case RequestType.SetGroupBan:
                        case RequestType.SetGroupAnonymousBan:
                        case RequestType.SetGroupWholeBan:
                        case RequestType.SetGroupAdmin:
                        case RequestType.SetGroupAnonymous:
                        case RequestType.SetGroupCard:
                        case RequestType.SetGroupLeave:
                        case RequestType.SetGroupSpecialTitle:
                        case RequestType.SetDiscussLeave:
                        case RequestType.SetFriendAddRequest:
                        case RequestType.SetGroupAddRequest:
                        case RequestType.SetRestart:
                        case RequestType.SetRestartPlugin:
                        case RequestType.CleanDataDir:
                        case RequestType.CleanPluginLog:
                        case RequestType.CheckUpdate:
                        case RequestType.HandleQuickOperation:
                            break;
                    }
                    this.SetResult(echo, data);
                }
                catch (Exception ex)
                {
                    await (OnErrorParseAsync?.Invoke(source.SelfId, new ResponseEventArgs(source, new OneBotContent() { RetCode = -1, Status = ex.ToString() })) ?? Task.CompletedTask);
                }
                return data;
            });
        }
        /// <summary>
        /// 狀態事件
        /// </summary>
        /// <param name="element"></param>
        /// <param name="source"></param>
        private async void StatusEvent(JsonElement element, Source source)
        {
            await (OnStatusAsync?.Invoke(source.SelfId, new StatusEventArgs(source, await ParseResponse(source, element))) ?? Task.CompletedTask);
        }
        /// <summary>
        /// 請求事件
        /// </summary>
        /// <param name="element"></param>
        /// <param name="source"></param>
        private async void RequestEvent(JsonElement element, Source source)
        {
            try
            {
                if (element.TryGetProperty("request_type", out JsonElement type))
                {
                    long userid;
                    string flag, comment;
                    if (element.TryGetProperty("user_id", out JsonElement je_uid)) { userid = je_uid.GetInt64(); } else { return; }
                    if (element.TryGetProperty("flag", out JsonElement je_flag)) { flag = je_flag.GetString(); } else { return; }
                    if (element.TryGetProperty("comment", out JsonElement je_cm)) { comment = je_cm.GetString(); } else { return; }
                    switch (type.GetString())
                    {
                        case "friend":
                            await (OnFriendRequestAsync?.Invoke(source.SelfId, new CqFriendRequestEventArgs(source, userid, flag, comment)) ?? Task.CompletedTask);
                            break;
                        case "group":
                            long groupid = 0;
                            string subtype = "";
                            if (element.TryGetProperty("sub_type", out JsonElement je_st)) { subtype = je_st.GetString(); }
                            if (element.TryGetProperty("group_id", out JsonElement je_gid)) { groupid = je_st.GetInt64(); }
                            await (OnGroupRequestAsync?.Invoke(source.SelfId, new CqGroupRequestEventArgs(source, userid, groupid, subtype, flag, comment)) ?? Task.CompletedTask);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                await (OnErrorParseAsync?.Invoke(source.SelfId, new ResponseEventArgs(source, new OneBotContent() { RetCode = -1, Status = ex.ToString() })) ?? Task.CompletedTask);
            }
        }
        /// <summary>
        /// 通知事件
        /// </summary>
        /// <param name="element"></param>
        /// <param name="source"></param>
        private async void NoticEvent(JsonElement element, Source source)
        {
            if (element.TryGetProperty("notice_type", out JsonElement type))
            {
                long userid, groupid = 0, operatorid = 0, duration = 0;
                string subtype = "";
                GroupNotic gnotic = GroupNotic.Upload;
                File file = null;
                if (element.TryGetProperty("user_id", out JsonElement je_uid)) { userid = je_uid.GetInt64(); } else { return; }
                if (element.TryGetProperty("operator_id", out JsonElement je_oi)) { operatorid = je_oi.GetInt64(); }
                if (element.TryGetProperty("group_id", out JsonElement je_di)) { groupid = je_di.GetInt64(); }
                if (element.TryGetProperty("duration", out JsonElement je_du)) { duration = je_du.GetInt64(); }
                if (element.TryGetProperty("sub_type", out JsonElement je_st)) { subtype = je_st.GetString(); }
                if (element.TryGetProperty("file", out JsonElement je_file))
                {
                    if (je_file.GetRawText() != "null")
                    {
                        if (je_file.TryGetProperty("id", out JsonElement je_fid) && je_file.TryGetProperty("name", out JsonElement je_fn) &&
                            je_file.TryGetProperty("size", out JsonElement je_fs) && je_file.TryGetProperty("busid", out JsonElement je_fb))
                        {
                            file = new File()
                            {
                                Id = je_fid.GetString(),
                                Name = je_fn.GetString(),
                                Size = je_fs.GetInt64(),
                                Busid = je_fb.GetInt64()
                            };
                        }

                    }
                }
                switch (subtype)
                {
                    case "set":
                        gnotic = GroupNotic.Admin | GroupNotic.Set;
                        break;
                    case "unset":
                        gnotic = GroupNotic.Admin | GroupNotic.UnSet;
                        break;
                    case "leave":
                        gnotic = GroupNotic.Decrease | GroupNotic.Leave;
                        break;
                    case "kick":
                        gnotic = GroupNotic.Decrease | GroupNotic.Kick;
                        break;
                    case "kick_me":
                        gnotic = GroupNotic.Decrease | GroupNotic.KickMe;
                        break;
                    case "approve":
                        gnotic = GroupNotic.Increase | GroupNotic.Approve;
                        break;
                    case "invite":
                        gnotic = GroupNotic.Increase | GroupNotic.Invite;
                        break;
                    case "ban":
                        gnotic = GroupNotic.Ban;
                        break;
                    case "lift_ban":
                        gnotic = GroupNotic.Lift_Ban;
                        break;
                }
                switch (type.GetString())
                {
                    case "group_upload":
                    case "group_admin":
                    case "group_decrease":
                    case "group_increase":
                    case "group_ban":
                        await (OnGroupNoticAsync?.Invoke(source.SelfId, new CqGroupNoticEventArgs(source, groupid, userid, operatorid, duration, file, gnotic)) ?? Task.CompletedTask);
                        break;
                    case "friend_add":
                        await (OnFriendAddAsync?.Invoke(source.SelfId, new CqFriendAddEventArgs(source, userid)) ?? Task.CompletedTask);
                        break;
                }
            }
        }
        /// <summary>
        /// 消息事件
        /// </summary>
        /// <param name="element"></param>
        /// <param name="source"></param>
        private async void MessageEvent(JsonElement element, Source source)
        {
            if (element.TryGetProperty("message_type", out JsonElement type))
            {
                if (element.TryGetProperty("sender", out JsonElement je_sender) == false) { return; }
                Anonymous anonymous = null;
                CqHttpSender sender = new CqHttpSender();
                long targetid = 0;
                string subtype = "";

                if (element.TryGetProperty("discuss_id", out JsonElement je_di)) { targetid = je_di.GetInt64(); }
                if (element.TryGetProperty("group_id", out JsonElement je_gi)) { targetid = je_gi.GetInt64(); }
                if (element.TryGetProperty("sub_type", out JsonElement je_st)) { subtype = je_st.GetString(); }
                if (je_sender.TryGetProperty("user_id", out JsonElement je_uid)) { sender.UserId = je_uid.GetInt64(); }
                if (je_sender.TryGetProperty("nickname", out JsonElement je_name)) { sender.NickName = je_name.GetString(); }
                if (je_sender.TryGetProperty("age", out JsonElement je_age)) { sender.Age = je_age.GetInt32(); }
                if (je_sender.TryGetProperty("sex", out JsonElement je_sex))
                {
                    switch (je_sex.GetString())
                    {
                        case "male":
                            sender.Sex = InGroupSex.Male;
                            break;
                        case "female":
                            sender.Sex = InGroupSex.Male;
                            break;
                        case "unknown":
                            sender.Sex = InGroupSex.Unknown;
                            break;
                    }
                }
                if (type.GetString() == "group")
                {
                    if (je_sender.TryGetProperty("anonymous", out JsonElement je_anonymous))
                    {
                        if (je_anonymous.ValueKind != JsonValueKind.Null)
                        {
                            if (je_sender.TryGetProperty("area", out JsonElement je_anonymousId))
                                if (je_sender.TryGetProperty("role", out JsonElement je_anonymousFlag))
                                    if (je_sender.TryGetProperty("title", out JsonElement je_anonymousName))
                                        anonymous = new Anonymous()
                                        {
                                            Id = je_anonymousId.GetInt64(),
                                            Flag = je_anonymousFlag.GetString(),
                                            Name = je_anonymousName.GetString()
                                        };
                        }
                    }
                    if (je_sender.TryGetProperty("role", out JsonElement je_role))
                    {
                        switch (je_role.GetString())
                        {
                            case "owner":
                                sender.Role = Model.GroupRole.Owner;
                                break;
                            case "admin":
                                sender.Role = Model.GroupRole.Admin;
                                break;
                            case "member":
                                sender.Role = Model.GroupRole.Member;
                                break;
                        }
                    }
                    if (je_sender.TryGetProperty("level", out JsonElement je_level))
                    {
                        sender.Level = je_level.GetString();
                    }
                    if (je_sender.TryGetProperty("card", out JsonElement je_card))
                    {
                        sender.Card = je_card.GetString();
                    }
                    if (je_sender.TryGetProperty("area", out JsonElement je_area))
                    {
                        sender.Area = je_area.GetString();
                    }
                    if (je_sender.TryGetProperty("title", out JsonElement je_title))
                    {
                        sender.Title = je_title.GetString();
                    }
                }
                CqHttpMessageEventArgs ea = new CqHttpMessageEventArgs(
                    message: element.GetProperty("message").GetString(),
                    rawMessage: element.GetProperty("raw_message").GetString(),
                    subType: subtype,
                    messageId: element.GetProperty("message_id").GetInt32(),
                    targetId: targetid,
                    fontId: element.GetProperty("font").GetInt64(),
                    sender: sender,
                    anonymous: anonymous,
                    source: source
                 );
                await (OnMessageAsync?.Invoke(source.SelfId, ea) ?? Task.CompletedTask);
                switch (type.GetString())
                {
                    case "private":
                        await (OnPrivateMessageAsync?.Invoke(source.SelfId, ea) ?? Task.CompletedTask);
                        break;
                    case "group":
                        await (OnGroupMessageAsync?.Invoke(source.SelfId, ea) ?? Task.CompletedTask);
                        break;
                    case "discuss":
                        await (OnDiscussMessageAsync?.Invoke(source.SelfId, ea) ?? Task.CompletedTask);
                        break;
                }
            }
        }
        /// <summary>
        /// 元事件
        /// </summary>
        /// <param name="element"></param>
        /// <param name="source"></param>
        private async void MetaEvent(JsonElement element, Source source)
        {
            if (element.TryGetProperty("meta_event_type", out JsonElement meta))
            {
                if (meta.GetString() == "heartbeat")
                {
                    if (element.TryGetProperty("status", out JsonElement status))
                    {
                        StatusEvent(status, source);
                    }
                }
                if (meta.GetString() == "lifecycle")
                {
                    if (element.TryGetProperty("sub_type", out JsonElement subType))
                    {
                        if (subType.GetString() == "connect")
                            await (OnConnectedAsync?.Invoke(source.SelfId, new ConnectEventArgs(source)) ?? Task.CompletedTask);
                    }
                }
            }
        }
    }
}
