using Chii.OneBot.SDK.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Event
{
    /// <summary>
    /// 元事件 - 狀態信息
    /// </summary>
    public sealed class StatusEventArgs : CqHttpBaseEventArgs
    {
        public ResponseResource Data { get; set; }

        public StatusEventArgs(Source source, ResponseResource data)
        {
            base.Source = source;
            this.Data = data;
        }
    }
}
