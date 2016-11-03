using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Core.Models.Forums
{
    /// <summary>
    /// 实体类——论坛帖类型记录信息
    /// </summary>
    [Description("论坛帖类型记录信息")]
    public class ForumType : EntityBase<int>
    {
        public ForumType()
        {
        }
        /// <summary>
        /// 论坛帖类型名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ForumTypeName { get; set; }

        /// <summary>
        /// 论坛帖类型名称
        /// </summary>
        [StringLength(200)]
        public string ForumTypeLogo { get; set; }

        /// <summary>
        /// 父级论坛帖类型
        /// </summary>
        public int ForumParentTypeID { get; set; }
        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual SystemUser User { get; set; }
        public int SystemUserID { get; set; }
    }
}
