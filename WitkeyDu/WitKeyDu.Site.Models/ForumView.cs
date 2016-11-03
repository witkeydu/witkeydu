using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Site.Models
{
    public class ForumView
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string ForumName { get; set; }

        /// <summary>
        /// 任务内容
        /// </summary>
        public string ForumContent { get; set; }

        /// <summary>
        /// 任务图标
        /// </summary>
        public string ForumLogo { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string ForumReleaser { get; set; }
    }
}
