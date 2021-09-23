///source : https://github.com/Jie2GG/Native.Framework
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Chii.OneBot.SDK.Enumeration
{
    /// <summary>
    /// 消息字段中的類型
    /// </summary>
    [DefaultValue(Function.Unknown)]
    public enum Function
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,
        /// <summary>
        /// QQ表情
        /// </summary>
        Face,
        /// <summary>
        /// Emoji表情
        /// </summary>
        Emoji,
        /// <summary>
        /// 原創表情
        /// </summary>
        Bface,
        /// <summary>
        /// 小表情
        /// </summary>
        Sface,
        /// <summary>
        /// 圖片
        /// </summary>
        Image,
        /// <summary>
        /// 語音
        /// </summary>
        Record,
        /// <summary>
        /// At
        /// </summary>
        At,
        /// <summary>
        /// 猜拳
        /// </summary>
        Rps,
        /// <summary>
        /// 擲骰子
        /// </summary>
        Dice,
        /// <summary>
        /// 戳一戳
        /// </summary>
        Shake,
        /// <summary>
        /// 音樂
        /// </summary>
        Music,
        /// <summary>
        /// 鏈接分享
        /// </summary>
        Share,
        /// <summary>
        /// 卡片消息
        /// </summary>
        Rich,
        /// <summary>
        /// 簽到
        /// </summary>
        Sign,
        /// <summary>
        /// 紅包
        /// </summary>
        Hb,
        /// <summary>
        /// 推薦
        /// </summary>
        Contact,
        /// <summary>
        /// 釐米秀
        /// </summary>
        Show,
        /// <summary>
        /// 位置分享
        /// </summary>
        Location,
        /// <summary>
        /// 匿名消息
        /// </summary>
        Anonymous
    }
}
