using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WitKeyDu.Site.Models
{
    /// <summary>
    /// 实体类——论坛帖类型记录信息
    /// </summary>
    [Description("论坛帖类型记录信息")]
    public class ForumTypeView
    {
        /// <summary>
        /// 论坛帖类型名称
        /// </summary>
        public int ForumTypeID { get; set; }
        /// <summary>
        /// 论坛帖类型名称
        /// </summary>
        public string ForumTypeName { get; set; }

        /// <summary>
        /// 论坛帖类型名称
        /// </summary>
        public string ForumTypeLogo { get; set; }

        /// <summary>
        /// 父级论坛帖类型
        /// </summary>
        public int ForumParentTypeID { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string ForumTypeURL { get; set; }

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public int UserKey { get; set; }
    }
}
