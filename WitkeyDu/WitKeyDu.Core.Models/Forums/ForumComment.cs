using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core.Models.Forums
{

    [Description("评帖信息")]
    public class ForumComment : EntityBase<int>
    {
        public ForumComment()
        { 
        
        }


        /// <summary>
        /// 评论内容
        /// </summary>
        [Required]
        [StringLength(3000)]
        public string CommentContent { get; set; }
        public int ParentCommentID { get; set; }
        public string CommentUserID { get; set; }
        public string CommentUserName { get; set; }
        
        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual SystemUser User { get; set; }

        public int SystemUserID { get; set; }
        public string UserName { get; set; }
        public string HeadImage { get; set; }

        /// <summary>
        ///     获取或设置 帖子类型信息集合
        /// </summary>
        public virtual Forum Forum { get; set; }

        public int ForumID { get; set; }
    }
}
