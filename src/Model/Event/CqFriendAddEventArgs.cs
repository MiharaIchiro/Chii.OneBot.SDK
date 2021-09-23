using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model.Event
{
    public sealed class CqFriendAddEventArgs : CqHttpBaseEventArgs
    {
        public long UserId { get; private set; }
        public CqFriendAddEventArgs(Source source, long userId)
        {
            base.Source = source;
            this.UserId = userId;
        }
    }
}
