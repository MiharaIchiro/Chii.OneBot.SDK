using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chii.OneBot.SDK.Model
{
    /// <summary>
    /// 群文件
    /// </summary>
    public sealed class File
    {
        /// <summary>
        /// 文件 ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件大小（字節數）
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// busid（目前不清楚有什麼作用）
        /// </summary>
        public long Busid { get; set; }
    }
}
