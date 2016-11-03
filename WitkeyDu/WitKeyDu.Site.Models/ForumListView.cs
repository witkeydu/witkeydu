using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Site.Models
{
    public class ForumListView
    {
        /// <summary>
        /// 帖子编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 帖子名称
        /// </summary>
        public string ForumName { get; set; }
        /// <summary>
        /// 帖子内容
        /// </summary>
        public string ForumContent { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime AddDate { get; set; }
        /// <summary>
        /// 任务图标
        /// </summary>
        public string ForumLogo { get; set; }
        /// <summary>
        /// 帖子点赞
        public int ForumPraise { get; set; }

        /// <summary>
        /// 帖子点赞
        /// </summary>
        public int ForumComment { get; set; }
    }
}
