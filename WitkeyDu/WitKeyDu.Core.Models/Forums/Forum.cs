using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Core.Models.Tasks;

namespace WitKeyDu.Core.Models.Forums
{
    [Description("论坛帖信息")]
    public class Forum : EntityBase<int>
    {
        public Forum()
        {
        }

        /// <summary>
        /// 帖子名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ForumName { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string ForumContent { get; set; }

        /// <summary>
        /// 帖子点赞
        /// </summary>
        public int ForumPraise { get; set; }

        /// <summary>
        /// 帖子点赞
        /// </summary>
        public int ForumComment { get; set; }

        /// <summary>
        /// 帖子图标
        /// </summary>
        [StringLength(200)]
        public string ForumLogo { get; set; }


        /// <summary>
        /// 获取或设置 用户拥有的角色信息集合
        /// </summary>
        public virtual ICollection<ForumComment> ForumComments { get; set; }

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual SystemUser User { get; set; }

        public int SystemUserID { get; set; }
        
        /// <summary>
        ///     获取或设置 帖子类型信息集合
        /// </summary>
        public virtual ForumType ForumType { get; set; }
        public int ForumTypeID { get; set; }

    }
}
