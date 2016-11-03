using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using WitKeyDu.Component.Tools;
using System.ComponentModel.DataAnnotations;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Core.Models.Files
{
    /// <summary>
    /// 实体类——论坛帖类型记录信息
    /// </summary>
    [Description("论坛帖类型记录信息")]
    public class FileType : EntityBase<int>
    {
        public FileType()
        {
        }
        /// <summary>
        /// 论坛帖类型名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FileTypeName { get; set; }

        /// <summary>
        /// 论坛帖类型名称
        /// </summary>
        [StringLength(200)]
        public string FileTypeLogo { get; set; }

        /// <summary>
        /// 父级论坛帖类型
        /// </summary>
        public int FileParentTypeID { get; set; }

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual SystemUser User { get; set; }
        public int SystemUserID { get; set; }

    }
}
