using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Core.Models.Files
{
    [Description("文件信息")]
    public class File : EntityBase<int>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public File()
        {
        }

        /// <summary>
        /// 文件编码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FileCode { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FileName { get; set; }

        /// <summary>
        /// 文件介绍
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FileIntroduction { get; set; }
        

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual SystemUser User { get; set; }
        public int SystemUserID { get; set; }

        /// <summary>
        ///     获取或设置 文件务类型信息集合
        /// </summary>
        public virtual FileType FileType { get; set; }
        public int FileTypeID { get; set; }

    }
}
