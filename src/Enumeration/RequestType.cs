﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Enumeration
{
    public enum RequestType
    {
        /// <summary>
        /// 發送消息
        /// </summary>
        [Description("send_msg")]
        SendMsg = 0,

        /// <summary>
        /// 撤回消息
        /// </summary>
        [Description("delete_msg")]
        DeleteMsg = 1,

        /// <summary>
        /// 發送好友贊
        /// </summary>
        [Description("send_like")]
        SendLike = 2,

        /// <summary>
        /// 群組踢人
        /// </summary>
        [Description("set_group_kick")]
        SetGroupKick = 3,

        /// <summary>
        /// 群組單人禁言
        /// </summary>
        [Description("set_group_ban")]
        SetGroupBan = 4,

        /// <summary>
        /// 群組匿名用戶禁言
        /// </summary>
        [Description("set_group_anonymous_ban")]
        SetGroupAnonymousBan = 5,

        /// <summary>
        /// 群組全員禁言
        /// </summary>
        [Description("set_group_whole_ban")]
        SetGroupWholeBan = 6,

        /// <summary>
        /// 群組設置管理員
        /// </summary>
        [Description("set_group_admin")]
        SetGroupAdmin = 7,

        /// <summary>
        /// 群組匿名
        /// </summary>
        [Description("set_group_anonymous")]
        SetGroupAnonymous = 8,

        /// <summary>
        /// 設置群名片（群備註）
        /// </summary>
        [Description("set_group_card")]
        SetGroupCard = 9,

        /// <summary>
        /// 退出群組
        /// </summary>
        [Description("set_group_leave")]
        SetGroupLeave = 10,

        /// <summary>
        /// 設置群組專屬頭銜
        /// </summary>
        [Description("set_group_special_title")]
        SetGroupSpecialTitle = 11,

        /// <summary>
        /// 退出討論組
        /// </summary>
        [Description("set_discuss_leave")]
        SetDiscussLeave = 12,

        /// <summary>
        /// 處理加好友請求
        /// </summary>
        [Description("set_friend_add_request")]
        SetFriendAddRequest = 13,

        /// <summary>
        /// 處理加群請求／邀請
        /// </summary>
        [Description("set_group_add_request")]
        SetGroupAddRequest = 14,

        /// <summary>
        /// 獲取登錄號信息
        /// </summary>
        [Description("get_login_info")]
        GetLoginInfo = 15,

        /// <summary>
        /// 獲取陌生人信息
        /// </summary>
        [Description("get_stranger_info")]
        GetStrangerInfo = 16,

        /// <summary>
        /// 獲取陌生人信息
        /// </summary>
        [Description("get_group_list")]
        GetGroupList = 17,

        /// <summary>
        /// 獲取陌生人信息
        /// </summary>
        [Description("get_group_member_info")]
        GetGroupMemberInfo = 18,

        /// <summary>
        /// 獲取陌生人信息
        /// </summary>
        [Description("get_group_member_list")]
        GetGroupMemberList = 19,

        /// <summary>
        /// 獲取 Cookies
        /// </summary>
        [Description("get_cookies")]
        GetCookies = 20,

        /// <summary>
        /// 獲取 CSRF Token
        /// </summary>
        [Description("get_csrf_token")]
        GetCsrfToken = 21,

        /// <summary>
        /// 獲取 QQ 相關接口憑證
        /// </summary>
        [Description("get_credentials")]
        GetCredentials = 22,

        /// <summary>
        /// 獲取語音
        /// </summary>
        [Description("get_record")]
        GetRecord = 23,

        /// <summary>
        /// 獲取圖片
        /// </summary>
        [Description("get_image")]
        GetImage = 24,

        /// <summary>
        /// 檢查是否可以發送圖片
        /// </summary>
        [Description("can_send_image")]
        CanSendImage = 25,

        /// <summary>
        /// 檢查是否可以發送語音
        /// </summary>
        [Description("can_send_record")]
        CanSendRecord = 26,

        /// <summary>
        /// 獲取插件運行狀態
        /// </summary>
        [Description("get_status")]
        GetStatus = 27,

        /// <summary>
        /// 獲取酷 Q 及 HTTP API 插件的版本信息
        /// </summary>
        [Description("get_version_info")]
        GetVersionInfo = 28,

        /// <summary>
        /// 重啟酷 Q，並以當前登錄號自動登錄（需勾選快速登錄）
        /// </summary>
        [Description("set_restart")]
        SetRestart = 29,

        /// <summary>
        /// 重啟 HTTP API 插件
        /// </summary>
        [Description("set_restart_plugin")]
        SetRestartPlugin = 30,

        /// <summary>
        /// 清理數據目錄
        /// </summary>
        [Description("clean_data_dir")]
        CleanDataDir = 31,

        /// <summary>
        /// 清理插件日誌
        /// </summary>
        [Description("clean_plugin_log")]
        CleanPluginLog = 32,

        /// <summary>
        /// 獲取好友列表
        /// </summary>
        [Description("_get_friend_list")]
        GetFriendList = 101,

        /// <summary>
        /// 獲取群信息
        /// </summary>
        [Description("_get_group_info")]
        GetGroupInfo = 102,

        /// <summary>
        /// 清理數據目錄
        /// </summary>
        [Description("_get_vip_info")]
        GetVipInfo = 103,

        [Description(".check_update")]
        /// <summary>
        /// 檢查更新
        /// </summary>
        CheckUpdate = -1,

        [Description(".handle_quick_operation")]
        /// <summary>
        /// 快速操作
        /// </summary>
        HandleQuickOperation = -2,
    }
}
