using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Event
{
    /// <summary>
    /// 元事件 - 連接
    /// </summary>
    public sealed class ConnectEventArgs : CqHttpBaseEventArgs
    {
        public ConnectEventArgs(Source source)
        {
            base.Source = source;
        }
    }
}
