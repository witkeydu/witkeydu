using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Site.Models
{
    public class ForumCommentView
    {

        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentContent { get; set; }
        /// <summary>
        /// 回复用户编号
        /// </summary>
        public string CommentUserID { get; set; }
        /// <summary>
        /// 父级评论编号
        /// </summary>
        public int ParentCommentID { get; set; }
        /// <summary>
        /// 回复用户姓名
        /// </summary>
        public string CommentUserName { get; set; }
        /// <summary>
        /// 评论用户编号
        /// </summary>
        public int SystemUserID { get; set; }
        /// <summary>
        /// 评论用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 评论用户头像
        /// </summary>
        public string HeadImage { get; set; }
        /// <summary>
        /// 评论帖子编号
        /// </summary>
        public int ForumID { get; set; }
        /// <summary>
        /// 评论编号
        /// </summary>
        public int ForumCommentID { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime AndDate { get; set; }

        public List<ForumCommentView> ChildrenForumComment { get; set; }


    }
}
