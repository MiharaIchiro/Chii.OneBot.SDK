using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Resource
{
    public static class ActionResource
    {
        /// <summary>
        /// 響應主題
        /// </summary>
        public static readonly ISubject<Tuple<string, ResponseResource>, Tuple<string, ResponseResource>> OneBotSubject
            = new Subject<Tuple<string, ResponseResource>>();
    }
}
