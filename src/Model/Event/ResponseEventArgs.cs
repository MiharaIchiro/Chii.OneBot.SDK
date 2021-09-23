using Chii.OneBot.SDK.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Event
{
    /// <summary>
    /// 元事件 - 回調
    /// </summary>
    public sealed class ResponseEventArgs : CqHttpBaseEventArgs
    {
        public OneBotContent Response { get; set; }

        public ResponseEventArgs(Source source, OneBotContent response)
        {
            base.Source = source;
            this.Response = response;
        }
    }
}
